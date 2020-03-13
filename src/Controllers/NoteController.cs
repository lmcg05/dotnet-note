using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using src.Data;
using src.Dto;
using src.Persistence.Model;
using src.Services;

namespace src.Controllers
{
    [ApiController]
    [Route("/note")]
    public class NoteController : ControllerBase
    {

        private readonly ILogger<NoteController> _logger;
        private readonly IMapper _mapper;
        private readonly NoteService _noteService;

        public NoteController(ILogger<NoteController> logger, IMapper mapper, NoteService noteService)
        {
            _logger = logger;
            _mapper = mapper;
            _noteService = noteService;
        }

        [HttpGet]
        [Route("{notebookId}")]
        public async Task<List<NoteWithoutNotebookDto>> GetAllNotesForNotebook([FromRoute] int notebookId)
        {
            List<Note> list = await _noteService.GetAllNotesInNoteBookAsync(notebookId);
            return _mapper.Map<List<NoteWithoutNotebookDto>>(list);
        }

        [HttpPost]
        [Route("{notebookId}")]
        public async Task AddANoteToNoteBookAsync([FromRoute] int notebookId, [FromBody] NoteTextDto noteDto)
        {
            Note note = _mapper.Map<Note>(noteDto);
            await _noteService.AddNoteToNoteBookAsync(notebookId, note);
        }

        [HttpPatch]
        public async Task ChangeNoteTextAsync([FromBody] NoteWithoutNotebookDto noteDto)
        {
            Note note = _mapper.Map<Note>(noteDto);
            await _noteService.ChangeTextInsideNoteAsync(note);
        }

        [HttpDelete]
        [Route("{noteId}")]
        public async Task<int> DelettyAsync([FromRoute] int noteId)
        {
            return await _noteService.DeleteNoteAsync(noteId);
        }
    }
}
