using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace myApplication
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, RecipeDataObserver
    {
        public RecipeModel RecipeModelInstnce = null;
        public RecipeDataContorller RecipeDataController;
        public HTTPController HttpController;

        // 状態が増えた場合はEnumもしくはStateパターンに
        public bool isConnectingServer = false;

        ObservableCollection<BaseUtility> listData = new ObservableCollection<BaseUtility>();


        public MainWindow()
        {
            InitializeComponent();

            this.RecipeDataController = new RecipeDataContorller();
            this.HttpController = new HTTPController();

            RecipeDataController.DeserializeJson();

            RecipeModelInstnce = RecipeModel.GetInstance();
            listData = this.RecipeModelInstnce.BaseUtilityList;
            RecipeModelInstnce.SetObserver(this);


            DataGridComboBox.ItemsSource = RecipeDataController.GetComboBoxSource();
            RecipeDataGrid.ItemsSource = listData;
        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            RecipeDataController.AddItem();
        }

        public void UpdateData()
        {
            listData = this.RecipeModelInstnce.BaseUtilityList;
            RecipeDataGrid.ItemsSource = listData;
        }

        private void RecipeDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataGrid dg = sender as DataGrid;

            if (dg != null)
            {
                int index = dg.SelectedIndex;

                if (dg.CurrentColumn != null)
                {
                    string header = dg.CurrentColumn.Header.ToString();

                    if (index > -1)
                    {
                        RecipeDataController.EditItem(index, header, e.EditingElement);
                    }
                }
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            RecipeDataController.SerializeJson();
        }

        private void ConnectionServerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isConnectingServer)
            {
                HTTP httpWindow = new HTTP();
                httpWindow.ShowDialog();

                if (httpWindow.isSelectOK)
                {
                    isConnectingServer = true;
                    HttpController.AsyncConnectionServer(httpWindow.ConnectionAddress);

                    // 別サーバに接続する場合はこちらのみ
                    HttpController.StartsUpServer(httpWindow.ConnectionAddress);
                }
            }
            else
            {
                // 切断処理
                HttpController.DisConnectServer();
                isConnectingServer = false;
                LabelChange(ConnectionServerButton);
                MessageBox.Show(Properties.Resources.MsgDisConnectServer, "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            LabelChange(ConnectionServerButton);
        }

        private void LabelChange(Button button)
        {
            if (isConnectingServer)
            {
                if (button == ConnectionServerButton)
                {
                    ConnectionServerButton.Content = Properties.Resources.DisConnectServer;
                }
            }
            else
            {
                if (button == ConnectionServerButton)
                {
                    ConnectionServerButton.Content = Properties.Resources.ConnectServer;
                }
            }
        }
    }
}
