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
    public class CheckOutViewModel : ViewModelBase
    {
        private readonly CheckOutService _checkOutService;
        private CheckOut _selectedCheckOut;
        private int _bookId;
        private string _borrowerName;
        private string _borrowerPhone;
        private DateTime _borrowDate;
        private DateTime _returnDate;
        private ObservableCollection<CheckOut> _checkOuts;

        public CheckOutViewModel(CheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
            LoadCheckOutsCommand = ReactiveCommand.CreateFromTask(LoadCheckOutsAsync);
            AddCheckOutCommand = ReactiveCommand.CreateFromTask(AddCheckOutAsync);
            UpdateCheckOutCommand = ReactiveCommand.CreateFromTask(UpdateCheckOutAsync);
            DeleteCheckOutCommand = ReactiveCommand.CreateFromTask(DeleteCheckOutAsync);
            LoadCheckOutsCommand.Execute().Subscribe();
        }

        public ObservableCollection<CheckOut> CheckOuts
        {
            get => _checkOuts;
            set => this.RaiseAndSetIfChanged(ref _checkOuts, value);
        }

        public CheckOut SelectedCheckOut
        {
            get => _selectedCheckOut;
            set => this.RaiseAndSetIfChanged(ref _selectedCheckOut, value);
        }

        public int BookId
        {
            get => _bookId;
            set => this.RaiseAndSetIfChanged(ref _bookId, value);
        }

        public string BorrowerName
        {
            get => _borrowerName;
            set => this.RaiseAndSetIfChanged(ref _borrowerName, value);
        }

        public string BorrowerPhone
        {
            get => _borrowerPhone;
            set => this.RaiseAndSetIfChanged(ref _borrowerPhone, value);
        }

        public DateTime BorrowDate
        {
            get => _borrowDate;
            set => this.RaiseAndSetIfChanged(ref _borrowDate, value);
        }

        public DateTime ReturnDate
        {
            get => _returnDate;
            set => this.RaiseAndSetIfChanged(ref _returnDate, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCheckOutsCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCheckOutCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateCheckOutCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteCheckOutCommand { get; }

        private async Task LoadCheckOutsAsync()
        {
            CheckOuts = new ObservableCollection<CheckOut>(await _checkOutService.GetAllCheckOutsAsync());
        }

        private async Task AddCheckOutAsync()
        {
            var newCheckOut = new CheckOut
            {
                BookId = BookId,
                BorrowerName = BorrowerName,
                BorrowerPhone = BorrowerPhone,
                BorrowDate = BorrowDate,
                ReturnDate = ReturnDate
            };
            await _checkOutService.AddCheckOutAsync(newCheckOut);
            await LoadCheckOutsAsync();
        }

        private async Task UpdateCheckOutAsync()
        {
            if (SelectedCheckOut != null)
            {
                SelectedCheckOut.BookId = BookId;
                SelectedCheckOut.BorrowerName = BorrowerName;
                SelectedCheckOut.BorrowerPhone = BorrowerPhone;
                SelectedCheckOut.BorrowDate = BorrowDate;
                SelectedCheckOut.ReturnDate = ReturnDate;
                await _checkOutService.UpdateCheckOutAsync(SelectedCheckOut);
                await LoadCheckOutsAsync();
            }
        }

        private async Task DeleteCheckOutAsync()
        {
            if (SelectedCheckOut != null)
            {
                await _checkOutService.DeleteCheckOutAsync(SelectedCheckOut.Id);
                await LoadCheckOutsAsync();
            }
        }
    }
}
