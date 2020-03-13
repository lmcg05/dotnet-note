using Microsoft.EntityFrameworkCore;
using src.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasOne(note => note.Notebook)
                .WithMany(notebook => notebook.Notes)
                .HasForeignKey(note => note.NoteBookId);

            // For Other relationships go to https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#many-to-many
        }

        public DbSet<Note> Note { get; set; }
        public DbSet<NoteBook> NoteBook { get; set; }

    }
}
