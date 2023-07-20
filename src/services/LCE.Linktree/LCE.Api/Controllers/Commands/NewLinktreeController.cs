using LCE.Core.Infrastructure;
using LCE.Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LCE.Api.Controllers.Commands;

[ApiController]
[Route("api/v1/[controller]")]
public class NewLinktreeController : ControllerBase
{
    private readonly ILogger<NewLinktreeController> _logger;
    private readonly ICommandDispatcher _commandDispatcher;
    
    public NewLinktreeController(ILogger<NewLinktreeController> logger, ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }


    [HttpPost]
    public async Task<ActionResult> NewLinktree(NewLinktreeCommand command)
    {
        try
        {
            var id = Guid.NewGuid();
            command.Id = id;

            await _commandDispatcher.SendAsync(command);

            return StatusCode(StatusCodes.Status201Created, new
            {
                Id = id,
                Message = "Linktree created successfully!",
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.Log(LogLevel.Warning, ex, "Invalid operation exception occurred");
            return BadRequest(new
            {
                Message = ex.Message
            });
        }
        catch(Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "An error occurred while creating the post!";
            
            _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);
            
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
}