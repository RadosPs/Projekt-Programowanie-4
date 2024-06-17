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
    public class BookCategoryViewModel : ViewModelBase
    {
        private readonly BookCategoryService _bookCategoryService;
        private BookCategory _selectedCategory;
        private string _categoryName;
        private ObservableCollection<BookCategory> _categories;

        public BookCategoryViewModel(BookCategoryService bookCategoryService)
        {
            _bookCategoryService = bookCategoryService;
            LoadCategoriesCommand = ReactiveCommand.CreateFromTask(LoadCategoriesAsync);
            AddCategoryCommand = ReactiveCommand.CreateFromTask(AddCategoryAsync);
            UpdateCategoryCommand = ReactiveCommand.CreateFromTask(UpdateCategoryAsync);
            DeleteCategoryCommand = ReactiveCommand.CreateFromTask(DeleteCategoryAsync);
            LoadCategoriesCommand.Execute().Subscribe();
        }

        public ObservableCollection<BookCategory> Categories
        {
            get => _categories;
            set => this.RaiseAndSetIfChanged(ref _categories, value);
        }

        public BookCategory SelectedCategory
        {
            get => _selectedCategory;
            set => this.RaiseAndSetIfChanged(ref _selectedCategory, value);
        }

        public string CategoryName
        {
            get => _categoryName;
            set => this.RaiseAndSetIfChanged(ref _categoryName, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCategoriesCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteCategoryCommand { get; }

        private async Task LoadCategoriesAsync()
        {
            Categories = new ObservableCollection<BookCategory>(await _bookCategoryService.GetAllCategoriesAsync());
        }

        private async Task AddCategoryAsync()
        {
            var newCategory = new BookCategory
            {
                Name = CategoryName
            };
            await _bookCategoryService.AddCategoryAsync(newCategory);
            await LoadCategoriesAsync();
        }

        private async Task UpdateCategoryAsync()
        {
            if (SelectedCategory != null)
            {
                SelectedCategory.Name = CategoryName;
                await _bookCategoryService.UpdateCategoryAsync(SelectedCategory);
                await LoadCategoriesAsync();
            }
        }

        private async Task DeleteCategoryAsync()
        {
            if (SelectedCategory != null)
            {
                await _bookCategoryService.DeleteCategoryAsync(SelectedCategory.Id);
                await LoadCategoriesAsync();
            }
        }
    }
}
