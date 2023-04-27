using System;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Dtos;
using NotificationService.Models;
using NotificationService.Repositories;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.RatingPublished:
                    addRating(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch(eventType.Event)
            {
                case "Rating_Published":
                    Console.WriteLine("--> Rating Published Event Detected");
                    return EventType.RatingPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addRating(string ratingPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                
                var ratingPublishedDto = JsonSerializer.Deserialize<RatingPublishedDto>(ratingPublishedMessage);

                try
                {
                    var rating = _mapper.Map<Rating>(ratingPublishedDto);
                    repo.CreateNotification(rating);
                    repo.SaveChanges();
                    Console.WriteLine("--> Rating added!");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Rating to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        RatingPublished,
        Undetermined
    }
}