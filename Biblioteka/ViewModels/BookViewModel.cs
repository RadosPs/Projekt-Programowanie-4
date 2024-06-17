using Biblioteka.Model;
using Biblioteka.Service;
using Biblioteka.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private readonly BookService _bookService;
        private Book _selectedBook;
        private string _title;
        private string _author;
        private int _categoryId;
        private ObservableCollection<Book> _books;

        public BookViewModel(BookService bookService)
        {
            _bookService = bookService;
            LoadBooksCommand = ReactiveCommand.CreateFromTask(LoadBooksAsync);
            AddBookCommand = ReactiveCommand.CreateFromTask(AddBookAsync);
            UpdateBookCommand = ReactiveCommand.CreateFromTask(UpdateBookAsync);
            DeleteBookCommand = ReactiveCommand.CreateFromTask(DeleteBookAsync);
            LoadBooksCommand.Execute().Subscribe();
        }

        public ObservableCollection<Book> Books
        {
            get => _books;
            set => this.RaiseAndSetIfChanged(ref _books, value);
        }

        public Book SelectedBook
        {
            get => _selectedBook;
            set => this.RaiseAndSetIfChanged(ref _selectedBook, value);
        }

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string Author
        {
            get => _author;
            set => this.RaiseAndSetIfChanged(ref _author, value);
        }

        public int CategoryId
        {
            get => _categoryId;
            set => this.RaiseAndSetIfChanged(ref _categoryId, value);
        }

        public ReactiveCommand<Unit, Unit> LoadBooksCommand { get; }
        public ReactiveCommand<Unit, Unit> AddBookCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateBookCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteBookCommand { get; }

        private async Task LoadBooksAsync()
        {
            Books = new ObservableCollection<Book>(await _bookService.GetAllBooksAsync());
        }

        private async Task AddBookAsync()
        {
            var newBook = new Book
            {
                Title = Title,
                Author = Author,
                CategoryId = CategoryId
            };
            await _bookService.AddBookAsync(newBook);
            await LoadBooksAsync();
        }

        private async Task UpdateBookAsync()
        {
            if (SelectedBook != null)
            {
                SelectedBook.Title = Title;
                SelectedBook.Author = Author;
                SelectedBook.CategoryId = CategoryId;
                await _bookService.UpdateBookAsync(SelectedBook);
                await LoadBooksAsync();
            }
        }

        private async Task DeleteBookAsync()
        {
            if (SelectedBook != null)
            {
                await _bookService.DeleteBookAsync(SelectedBook.Id);
                await LoadBooksAsync();
            }
        }
    }
}
