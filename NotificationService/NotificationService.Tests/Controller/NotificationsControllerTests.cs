using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Controllers;
using NotificationService.Dtos;
using NotificationService.Models;
using NotificationService.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NotificationService.Tests.Controller
{
    public class NotificationsControllerTests
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;
        public NotificationsControllerTests()
        {
            _repository = A.Fake<INotificationRepository>();
            _mapper = A.Fake<IMapper>();
        }


        [Fact]
        public void NotificationsController_GetUnackedNotifications_ReturnOK()
        {

            //Arrange
            var ratingItems = A.Fake<IEnumerable<Rating>>();
            var ratingList = A.Fake<IEnumerable<RatingReadDto>>();
            A.CallTo(() => _mapper.Map<IEnumerable<RatingReadDto>>(ratingItems)).Returns(ratingList);
            var controller = new NotificationsController(_repository, _mapper);

            //Act

            var result = controller.GetUnackedNotifications();



            //Assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<IEnumerable<RatingReadDto>>));



        }




    }
}
