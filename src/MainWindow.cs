using System.Windows;
using Pokedex.Pokerole.Models;

namespace Pokedex.Pokerole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(Dispatcher);
        }
    }
}