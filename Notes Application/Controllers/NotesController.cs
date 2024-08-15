using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Notes_Application.Models;
using AuthProvider;
using AuthProvider.Authentication.Authorizers;
using AuthProvider.AuthModelBinder;

namespace Notes_Application.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class NotesController : ControllerBase
{
    private static readonly Dictionary<Guid, Note> UserNotes = [];

    public NotesController()
    {

    }

    [HttpGet]
    [Auth<SessionAuth>]
    public IActionResult GetNotes([FromAuth<AuthUserId>] Guid userId)
    {
        if (UserNotes.TryGetValue(userId, out Note? note))
        {
            return Ok(note);
        } else
        {
            note = new Note() { Content = "New Note" };
            UserNotes[userId] = note;
            return Ok(note);
        }
    }

    [HttpPost]
    [Auth<SessionAuth>]
    public IActionResult PostNotes([FromAuth<AuthUserId>] Guid userId, [FromBody] Note note)
    {
        if (UserNotes.TryGetValue(userId, out Note? userNote))
        {
            userNote.Content = note.Content;
        }
        else
        {
            var newNote = new Note() { Content = note.Content };
            UserNotes[userId] = newNote;
        }

        return Ok();
    }
}
