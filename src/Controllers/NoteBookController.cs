using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using src.Dto;
using src.Persistence.Model;
using src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Controllers
{
    [ApiController]
    [Route("/notebook")]
    public class NoteBookController: ControllerBase
    {
        private readonly NoteBookService _noteBookService;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteBookController> _logger;

        public NoteBookController(ILogger<NoteBookController> logger, NoteBookService noteBookService, IMapper mapper)
        {
            _logger = logger;
            _noteBookService = noteBookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<NoteBookWithoutNotesDto>> GetAllNoteBooksAsync()
        {
            List<NoteBook> notebooks = await _noteBookService.GetAllNoteBooks();
            return _mapper.Map<List<NoteBookWithoutNotesDto>>(notebooks);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<NoteBookDto> GetNotesByNoteBookIdAsync([FromRoute] int id)
        {
            NoteBook notebook = await _noteBookService.GetNoteBook(id);
            return _mapper.Map<NoteBookDto>(notebook);
        }

        [HttpPost]
        public async Task CreateNoteBookAsync(NoteBookTitleDto noteBookTitleDto)
        {
            NoteBook notebook = _mapper.Map<NoteBook>(noteBookTitleDto);
            await _noteBookService.CreateNoteBook(notebook);
        }

        [HttpPatch]
        public async Task ChangeNoteBookName(NoteBookWithoutNotesDto noteBookWithoutNotesDto)
        {
            NoteBook noteBook = _mapper.Map<NoteBook>(noteBookWithoutNotesDto);
            await _noteBookService.PatchNotebookTitle(noteBook);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<int> DeleteNoteBookAsync([FromRoute] int id)
        {
            return await _noteBookService.DeleteNoteBook(id);
        }
    }
}
