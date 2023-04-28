using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RatingService.Repositories;
using System.Collections.Generic;
using System;
using RatingService.Dtos;
using RatingService.SyncDataServices.Http;
using System.Threading.Tasks;
using RatingService.Models;
using RatingService.AsyncDataServices;

namespace RatingService.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IServiceProviderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;


        public RatingsController(IServiceProviderRepository repository
            ,IMapper mapper
            ,ICommandDataClient commandDataClient
            ,IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }


        [HttpGet("/api/ServiceProviders/", Name = "GetServiceProviders")]
        public ActionResult<IEnumerable<ServiceProviderReadDto>> GetServiceProviders()
        {
            Console.WriteLine("--> Getting Platforms from CommandsService");

            var serviceProviderItems = _repository.GetAllServiceProviders();

            return Ok(_mapper.Map<IEnumerable<ServiceProviderReadDto>>(serviceProviderItems));
        }


        [HttpGet("/api/ServiceProviders/{id}", Name = "GetServiceProviderById")]
        public ActionResult<ServiceProviderReadDto> GetServiceProviderById(int id)
        {
            var serviceProviderItem = _repository.GetServiceProviderById(id);
            if (serviceProviderItem != null)
            {
                return Ok(_mapper.Map<ServiceProviderReadDto>(serviceProviderItem));
            }

            return NotFound();
        }


        [HttpGet("/api/ServiceProviders/{id}/Ratings", Name = "GetServiceProviderRatingsById")]
        public ActionResult<ServiceProviderReadDto> GetServiceProviderRatingsById(int id)
        {

            if (!_repository.ServiceProviderExists(id))
            {
                return NotFound();
            }

            var serviceProviderItem = _repository.GetRatingsForServiceProvider(id);
            if (serviceProviderItem != null)
            {
                return Ok((serviceProviderItem));
            }

            return NotFound();
        }

        [HttpGet("/api/ServiceProviders/{id}/AverageRating", Name = "GetServiceProviderAverageRatingById")]
        public ActionResult<ServiceProviderReadDto> GetServiceProviderAverageRatingById(int id)
        {

            if (!_repository.ServiceProviderExists(id))
            {
                return NotFound();
            }

            var serviceProviderItem = _repository.GetServiceProviderWithAverageRatingById(id);
            if (serviceProviderItem != null)
            {
                return Ok((serviceProviderItem));
            }

            return NotFound();
        }


        [HttpPost("/api/ServiceProviders", Name = "CreateServiceProvider")]
        public ActionResult<ServiceProviderReadDto> CreateServiceProvider(ServiceProviderCreateDto serviceProviderCreateDto)
        {
            var ServiceProviderModel = _mapper.Map<ServiceProvider>(serviceProviderCreateDto);
            _repository.CreateServiceProvider(ServiceProviderModel);
            _repository.SaveChanges();

            var serviceProviderReadDto = _mapper.Map<ServiceProviderReadDto>(ServiceProviderModel);

            return CreatedAtRoute(nameof(GetServiceProviderById), new { Id = serviceProviderReadDto.Id }, serviceProviderReadDto);
        }


        [HttpGet("/api/Ratings/", Name = "GetRatings")]
        public ActionResult<IEnumerable<RatingReadDto>> GetRatings()
        {
            Console.WriteLine("--> Getting Platforms....");

            var ratingItem = _repository.GetRatings();

            return Ok(_mapper.Map<IEnumerable<RatingReadDto>>(ratingItem));
        }


        [HttpGet("/api/Ratings/{id}", Name = "GetRatingById")] 
        public ActionResult<RatingReadDto> GetRatingById(int id)
        {
            var ratingItem = _repository.GetRatingById(id);
            if (ratingItem != null)
            {
                return Ok(_mapper.Map<RatingReadDto>(ratingItem));
            }

            return NotFound();
        }


        [HttpPost("/api/Ratings/", Name = "CreateRating")]
        public async Task<ActionResult<RatingReadDto>> CreateRating(RatingCreateDto ratingCreateDto)
        {
            if (!_repository.ServiceProviderExists(ratingCreateDto.ServiceProviderId))
            {
                return NotFound();
            }


            var ratingModel = _mapper.Map<Rating>(ratingCreateDto);

            _repository.CreateRating(ratingModel);
            _repository.SaveChanges();

            var ratingReadDto = _mapper.Map<RatingReadDto>(ratingModel);

            //Send Sync Message http request to other service
            try
            {
                await _commandDataClient.SendRatingToNotification(ratingReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            //Send Async Message
            try
            {
                var ratingPublishedDto = _mapper.Map<RatingPublishedDto>(ratingReadDto);
                ratingPublishedDto.Event = "Rating_Published";
                _messageBusClient.PublishNewRating(ratingPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(CreateRating), new { Id = ratingReadDto.Id }, ratingReadDto);
        }

    }
}
