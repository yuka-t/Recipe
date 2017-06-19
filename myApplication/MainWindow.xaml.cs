using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace myApplication
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, RecipeDataObserver
    {
        public RecipeModel RecipeModelInstnce = null;
        public RecipeDataContorller RecipeDataController;
        ObservableCollection<BaseUtility> listData = new ObservableCollection<BaseUtility>();


        public MainWindow()
        {
            InitializeComponent();

            this.RecipeDataController = new RecipeDataContorller();
            RecipeDataController.DeserializeJson();

            RecipeModelInstnce = RecipeModel.GetInstance();
            listData = this.RecipeModelInstnce.BaseUtilityList;
            RecipeModelInstnce.SetObserver(this);


            DataGridComboBox.ItemsSource = RecipeDataController.GetComboBoxSource();
            RecipeDataGrid.ItemsSource = listData;

            Debug.WriteLine("★★★Main.End");
        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            RecipeDataController.AddItem();
        }

        public void UpdateData()
        {
            //
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //RecipeDataController.DeserializeJson();
        }
    }
}
