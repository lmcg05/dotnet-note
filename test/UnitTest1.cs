using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using src.Controllers;
using src.Dto;
using src.Persistence.Model;
using src.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace test
{
    public class UnitTest1
    {
        [Fact]
        public void TestingCorrectResponseWithControllerAsync()
        {
            // Arrange
            var noteServiceMock = new Mock<NoteService>(null, null);
            NoteBook notebook = new NoteBook() { Id = 1, Title = "first notebook" };
            List<Note> notes = new List<Note>();
            notes.Add(new Note() { Id = 1, Text = "Hello", NoteBookId = 1, Notebook = notebook });
            notes.Add(new Note() { Id = 2, Text = "Hello again", NoteBookId = 1, Notebook = notebook });

            notebook.Notes = notes;

            noteServiceMock.Setup(service => service.GetAllNotes())
                .ReturnsAsync(notes);

            var autoMapperMock = new Mock<IMapper>();
            List<NoteDto> noteDtos = new List<NoteDto>();
            noteDtos.Add(new NoteDto() { Id = 1, Text = "Hello" });
            noteDtos.Add(new NoteDto() { Id = 2, Text = "Hello again" });

            autoMapperMock.Setup(mapper => mapper.Map<List<NoteDto>>(notes))
                .Returns(noteDtos);

            var loggerMock = new Mock<ILogger<NoteController>>();

            var noteController = new NoteController(logger: loggerMock.Object, mapper: autoMapperMock.Object, noteService: noteServiceMock.Object);

            // Act
            List<NoteDto> noteList = noteController.GetAll().Result;

            // Assert
            Assert.Equal(noteDtos, noteList);

        }

        [Fact]
        public void TestingAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Note, NoteDto>();
            });

            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();

            
            var noteServiceMock = new Mock<NoteService>(null, null);
            NoteBook notebook = new NoteBook() { Id = 1, Title = "first notebook" };
            List<Note> notes = new List<Note>();
            notes.Add(new Note() { Id = 1, Text = "Hello", NoteBookId = 1, Notebook = notebook });
            notes.Add(new Note() { Id = 2, Text = "Hello again", NoteBookId = 1, Notebook = notebook });

            notebook.Notes = notes;


            List<NoteDto> noteDtos = new List<NoteDto>();
            noteDtos.Add(new NoteDto() { Id = 1, Text = "Hello" });
            noteDtos.Add(new NoteDto() { Id = 2, Text = "Hello again" });


            // Act
            List<NoteDto> mappedDtos = mapper.Map<List<NoteDto>>(notes);

            // Assert
            Assert.Equal(noteDtos, mappedDtos);

        }
    }
}
