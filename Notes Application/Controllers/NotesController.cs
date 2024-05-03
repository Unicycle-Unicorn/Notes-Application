using Microsoft.AspNetCore.Mvc;
using Notes_Application.Models;

namespace Notes_Application.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController : ControllerBase
{

    private static readonly Note Note = new("Something");

    public NotesController()
    {

    }

    [HttpGet(Name = "Get")]
    public IActionResult Get()
    {
        return Ok(Note);
    }

    [HttpPost(Name = "Post")]
    public IActionResult Post([FromBody] Note note)
    {
        Note.Content = note.Content;
        return Ok();
    }
}
