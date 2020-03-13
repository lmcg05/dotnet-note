using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Persistence.Model
{
    /// <summary>
    /// Principle Entity
    /// </summary>
    public class NoteBook
    {

        /// <summary>
        /// Principle Key
        /// </summary>
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// collection navigation property
        /// </summary>
        public List<Note> Notes { get; set; } = new List<Note>();

    }
}
