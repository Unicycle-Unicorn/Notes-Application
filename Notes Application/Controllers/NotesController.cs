using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Notes_Application.Models;
using AuthProvider;
using AuthProvider.Authentication.Authorizers;

namespace Notes_Application.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class NotesController : ControllerBase
{

    private static readonly Note Note = new() { Content = "something" };

    public NotesController()
    {

    }

    [HttpGet]
    [Auth<SessionAuth>]
    public IActionResult GetNotes()
    {
        return Ok(Note);
    }

    [HttpPost]
    public IActionResult PostNotes([FromBody] Note note)
    {
        Note.Content = note.Content;
        return Ok();
    }
}
