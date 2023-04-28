using AutoMapper;
using RatingService.AsyncDataServices;
using RatingService.Repositories;
using RatingService.SyncDataServices.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;
using AutoMapper.Configuration.Annotations;
using RatingService.Dtos;
using RatingService.Models;
using RatingService.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace RatingService.Tests.Controller
{
    public class RatingsControllerTests
    {
        private readonly IServiceProviderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public RatingsControllerTests()
        {
            _repository = A.Fake<IServiceProviderRepository>();
            _mapper = A.Fake<IMapper>();
            _commandDataClient = A.Fake<ICommandDataClient>();
            _messageBusClient= A.Fake<IMessageBusClient>();

        }


        [Fact]
        public void RatingsController_GetServiceProviders_ReturnsOK()
        {
            //Arrange
            var serviceProviderItems = A.Fake<IEnumerable<ServiceProvider>>();
            var serviceProviderdto = A.Fake<IEnumerable<ServiceProviderReadDto>>();
            A.CallTo(() => _mapper.Map<IEnumerable<ServiceProviderReadDto>>(serviceProviderItems)).Returns(serviceProviderdto);
            var controller = new RatingsController(_repository, _mapper, _commandDataClient, _messageBusClient);

            //Act

            var result = controller.GetServiceProviders();


            //Assert

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));

        }

        [Fact]
        public void RatingsController_GetServiceProviderById_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var serviceProviderItems = A.Fake<IEnumerable<ServiceProvider>>();
            var serviceProviderdto = A.Fake<IEnumerable<ServiceProviderReadDto>>();
            A.CallTo(() => _mapper.Map<IEnumerable<ServiceProviderReadDto>>(serviceProviderItems)).Returns(serviceProviderdto);
            var controller = new RatingsController(_repository, _mapper, _commandDataClient, _messageBusClient);

            //Act

            var result = controller.GetServiceProviderById(id);


            //Assert

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void RatingsController_GetServiceProviderAverageRatingById_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var serviceProviderItem = A.Fake<ServiceProvider>();
            A.CallTo(() => _repository.ServiceProviderExists(id)).Returns(true);
            var controller = new RatingsController(_repository, _mapper, _commandDataClient, _messageBusClient);

            //Act

            var result = controller.GetServiceProviderAverageRatingById(id);


            //Assert

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }


        [Fact]
        public void RatingsController_GetServiceProviderAverageRatingById_ReturnsNotFound()
        {
            //Arrange
            int id = 1;
            var serviceProviderItem = A.Fake<ServiceProvider>();
            A.CallTo(() => _repository.ServiceProviderExists(id)).Returns(false);
            var controller = new RatingsController(_repository, _mapper, _commandDataClient, _messageBusClient);

            //Act

            var result = controller.GetServiceProviderAverageRatingById(id);


            //Assert

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(NotFoundResult));
        }


    }
}
