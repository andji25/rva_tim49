using System.Windows;
using TouristDestinations_Component1.ViewModels;

namespace TouristDestinations_Component1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            vm?.StopWcfService();
            base.OnClosing(e);
        }
    }
}
