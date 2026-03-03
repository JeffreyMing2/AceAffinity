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

namespace AceAffinity.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainController _mainController;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _mainController = new MainController();
        }

        /// <summary>
        /// Handles the Click event of the OptimizeButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OptimizeButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "Status: Optimizing...";
            
            // Read the process name from the TextBox.
            string processName = ProcessNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(processName))
            {
                StatusTextBlock.Text = "Status: Process name cannot be empty.";
                return;
            }

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
