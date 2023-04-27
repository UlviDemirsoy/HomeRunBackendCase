using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Dtos;
using NotificationService.Repositories;
using System;
using System.Collections.Generic;

namespace NotificationService.Controllers
{
    [Route("api/n/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {

        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;

        public NotificationsController(INotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Notifications Service");

            return Ok("Inbound test of from Ratings Controler");
        }

        [HttpGet]
        public ActionResult<IEnumerable<RatingReadDto>> GetUnackedNotifications()
        {
            Console.WriteLine("--> Getting Ratings from RatingService");

            var ratingItems = _repository.GetUnackedNotifications();

            _repository.AckAllNotifications();
            _repository.SaveChanges();

            return Ok(_mapper.Map<IEnumerable<RatingReadDto>>(ratingItems));
        }


    }
}
