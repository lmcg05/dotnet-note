using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dto
{
    public class NoteBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<NoteWithoutNotebookDto> Notes { get; set; }
    }
}
