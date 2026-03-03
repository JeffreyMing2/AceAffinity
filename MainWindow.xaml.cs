using AceAffinity.Application;
using AceAffinity.Core.WinApiWrapper;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AceAffinity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainController _mainController;

        public MainWindow()
        {
            InitializeComponent();
            _mainController = new MainController();
        }

        private void OptimizeButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "Status: Optimizing...";
            
            // The process name for "ACE-Tray"
            string processName = "ACE-Tray";

            bool success = _mainController.OptimizeProcess(processName, NativeMethods.PriorityClass.IDLE_PRIORITY_CLASS);

            if (success)
            {
                StatusTextBlock.Text = $"Status: Successfully optimized '{processName}'.";
            }
            else
            {
                StatusTextBlock.Text = $"Status: Failed to optimize '{processName}'. (Is it running?)";
            }
        }
    }
}
