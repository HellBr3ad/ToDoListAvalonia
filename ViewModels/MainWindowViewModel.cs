using AvaloniaApplication2.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System;

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

        public MainWindowViewModel()
        {
            AddNoteCommand = ReactiveCommand.Create(AddNote);
            DeleteNoteCommand = ReactiveCommand.Create(DeleteNote);

            // Uncomment these lines for testing with default notes
            //Notes.Add(new Note { Title = "Sample Note 1", Content = "This is the content of the first sample note.", DateCreated = DateTime.Now, DateModified = DateTime.Now });
            //Notes.Add(new Note { Title = "Sample Note 2", Content = "This is the content of the second sample note.", DateCreated = DateTime.Now, DateModified = DateTime.Now });
            //Notes.Add(new Note { Title = "Sample Note 3", Content = "This is the content of the third sample note.", DateCreated = DateTime.Now, DateModified = DateTime.Now });

            //SelectedNote = Notes[0];
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
        }

        private void DeleteNote()
        {
            if (SelectedNote != null)
            {
                Notes.Remove(SelectedNote);
                SelectedNote = null;
            }
        }
    }
}
