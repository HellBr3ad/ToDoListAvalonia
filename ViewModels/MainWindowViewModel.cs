using AvaloniaApplication2.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvaloniaApplication2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Note? _selectedNote;
        private string? _newNoteTitle;
        private string? _newNoteContent;

        public ObservableCollection<Note> Notes { get; } = new ObservableCollection<Note>();

        public Note? SelectedNote
        {
            get => _selectedNote;
            set => this.RaiseAndSetIfChanged(ref _selectedNote, value);
        }

        public string? NewNoteTitle
        {
            get => _newNoteTitle;
            set => this.RaiseAndSetIfChanged(ref _newNoteTitle, value);
        }

        public string? NewNoteContent
        {
            get => _newNoteContent;
            set => this.RaiseAndSetIfChanged(ref _newNoteContent, value);
        }

        public ReactiveCommand<Unit, Unit> AddNoteCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteNoteCommand { get; }

        private readonly string saveFilePath = "notes.json";  // Path to save the notes

        public MainWindowViewModel()
        {
            // Load notes from file when the application starts
            LoadNotes();

            AddNoteCommand = ReactiveCommand.Create(AddNote);
            DeleteNoteCommand = ReactiveCommand.Create(DeleteNote);

            // Save the notes whenever the collection changes
            Notes.CollectionChanged += (s, e) => SaveNotes();
        }

        private void AddNote()
        {
            var newNote = new Note
            {
                Title = NewNoteTitle ?? "Untitled",
                Content = NewNoteContent ?? string.Empty,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            Notes.Add(newNote);
            SelectedNote = newNote; // Select the newly added note

            // Clear the input fields
            NewNoteTitle = string.Empty;
            NewNoteContent = string.Empty;

            // Save notes after adding
            SaveNotes();
        }

        private void DeleteNote()
        {
            if (SelectedNote != null)
            {
                Notes.Remove(SelectedNote);
                SelectedNote = null;

                // Save notes after deleting
                SaveNotes();
            }
        }

        // Change the method visibility to public to make them accessible
        public void SaveNotes()
        {
            var notesList = new List<Note>(Notes);
            var json = JsonConvert.SerializeObject(notesList, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(saveFilePath, json);
        }

        public void LoadNotes()
        {
            if (System.IO.File.Exists(saveFilePath))
            {
                var json = System.IO.File.ReadAllText(saveFilePath);
                var notesList = JsonConvert.DeserializeObject<List<Note>>(json);

                if (notesList != null)
                {
                    Notes.Clear();
                    foreach (var note in notesList)
                    {
                        Notes.Add(note);
                    }
                }
            }
        }
    }
}
