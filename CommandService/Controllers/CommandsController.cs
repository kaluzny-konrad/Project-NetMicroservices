using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommandService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> Get(int platformId)
        {
            Console.WriteLine($"--> Hit get for platformId: {platformId}");
            if (!_repo.PlatformExists(platformId)) return NotFound();
            var commands = _repo.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit get for platformId: {platformId} / commandId: {commandId}");
            if (!_repo.PlatformExists(platformId)) return NotFound();
            var command = _repo.GetCommand(platformId, commandId);
            if (command == null) return NotFound();
            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"--> Hit post for platformId: {platformId}");
            if (!_repo.PlatformExists(platformId)) return NotFound();
            var command = _mapper.Map<Command>(commandCreateDto);
            _repo.CreateCommand(platformId, command);
            _repo.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(command);
            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId, commandId = commandReadDto.Id }, commandReadDto);
        }
    }
}
