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
            StatusTextBlock.Text = "状态: 正在优化...";
            
            // Read the process name from the TextBox.
            string processName = ProcessNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(processName))
            {
                StatusTextBlock.Text = "状态: 进程名称不能为空。";
                return;
            }

            bool success = _mainController.OptimizeProcess(processName, NativeMethods.PriorityClass.IDLE_PRIORITY_CLASS);

            if (success)
            {
                StatusTextBlock.Text = $"状态: 成功优化进程 '{processName}'。";
            }
            else
            {
                StatusTextBlock.Text = $"状态: 无法优化 '{processName}'。(该进程是否正在运行？)";
            }
        }

        /// <summary>
        /// Handles the Click event of the AboutButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWin = new AboutWindow();
            aboutWin.Owner = this;
            aboutWin.ShowDialog();
        }
    }
}
