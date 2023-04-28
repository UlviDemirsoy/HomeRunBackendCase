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
            _commandDataClient = A.



        }


    }
}
