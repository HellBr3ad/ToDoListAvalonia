using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

namespace AvaloniaApplication2.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainWindowViewModel();
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel viewModel)
            {
                viewModel.SaveNotes();
            }
        }

        private void OnLoadButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel viewModel)
            {
                viewModel.LoadNotes();
            }
        }
    }
}
