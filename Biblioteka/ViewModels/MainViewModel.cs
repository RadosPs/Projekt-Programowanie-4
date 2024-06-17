

using ReactiveUI;

namespace Biblioteka.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly BookViewModel _bookViewModel;
        private readonly BookCategoryViewModel _bookCategoryViewModel;
        private readonly CheckOutViewModel _checkOutViewModel;

        public MainWindowViewModel(BookViewModel bookViewModel, BookCategoryViewModel bookCategoryViewModel, CheckOutViewModel checkOutViewModel)
        {
            _bookViewModel = bookViewModel;
            _bookCategoryViewModel = bookCategoryViewModel;
            _checkOutViewModel = checkOutViewModel;
        }

        public BookViewModel BookViewModel => _bookViewModel;
        public BookCategoryViewModel BookCategoryViewModel => _bookCategoryViewModel;
        public CheckOutViewModel CheckOutViewModel => _checkOutViewModel;
    }
}
