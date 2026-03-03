using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace AceAffinity.UI.Views
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutWindow"/> class.
        /// </summary>
        public AboutWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the CloseButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the mouse click on the project URL to open it in a browser.
        /// </summary>
        private void Hyperlink_RequestNavigate(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = "https://github.com/JeffreyMing2/AceAffinity";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法打开链接: {ex.Message}");
            }
        }
    }
}
