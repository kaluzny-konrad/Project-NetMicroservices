using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using System.Text.Json;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    addPlatform(message);
                    break;
                default: break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var genericEventDto = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (genericEventDto?.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("--> Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("--> Could not determine event type");
                    return EventType.Undetermined;
            }
        }

        private void addPlatform(string platformPublishedMessage)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
            var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
            try
            {
                var platform = _mapper.Map<Platform>(platformPublishedDto);
                if(repo.ExternalPlatformExists(platform.ExternalId))
                {
                    Console.WriteLine("--> Platform already exists...");
                    return;
                }
                repo.CreatePlatform(platform);
                repo.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
            }
        }
    }
}
