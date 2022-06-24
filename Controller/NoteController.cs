using Microsoft.AspNetCore.Mvc;
using Dapper;

using TodoBackend;

namespace TodoAppBackend.Controller
{
    public class NoteController : Controller
    {
    
        [Route("notes/today/{cur_date}")]
        public IActionResult GetNotesForToday(DateTime? cur_date)
        {
            return Query<Note>("select * from get_today_notes(@cur_date)", new { cur_date = cur_date });
        }

        [HttpPost]
        [Route("note/create")]
        public IActionResult CreateNote([FromBody] Note note)
        {
            return Execute("select * from insert_note(@title, @content)", note);
        }
        
        [HttpPost]
        [Route("note/{id}/update")]
        public IActionResult UpdateNote(int id, [FromBody] Note note)
        {
            if(note == null)
                throw new Exception("null note");

            return Execute("update notes set title = @title, content = @content where id = @id", note);
        }

        [Route("note/{id}/delete")]
        public IActionResult DeleteNote(int id)
        {
            return Execute("delete from notes where id = @id", new { id = id});
        }
    }
}