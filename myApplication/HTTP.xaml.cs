using System;
using System.Windows;

namespace myApplication
{
    /// <summary>
    /// HTTP.xaml の相互作用ロジック
    /// </summary>
    public partial class HTTP : Window
    {
        public string ConnectionAddress = "";
        public const string DefaultAddress = "http://localhost:8080/";
        public bool isSelectOK = false;

        public HTTP()
        {
            InitializeComponent();

            HTTPLabel.Content = Properties.Resources.MsgHTTPDialog;
            HTTPTextBox.Text = DefaultAddress;
            HTTPTextBox.Focus();
            HTTPTextBox.Select(0, HTTPTextBox.Text.Length);
        }

        private void HTTPOKButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionAddress = HTTPTextBox.Text;
            isSelectOK = true;
            this.Close();
        }

        private void HTTPCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
