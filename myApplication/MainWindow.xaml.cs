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

namespace myApplication
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public RecipeModel RecipeModel = RecipeModel.GetInstance();

        public MainWindow()
        {
            InitializeComponent();

            DataGridComboBox.ItemsSource = GetComboBoxSource();
            RecipeDataGrid.ItemsSource = this.RecipeModel.BaseUtilityList;

        }

        public List<BaseUtility.BaseEnum> GetComboBoxSource()
        {
            List<BaseUtility.BaseEnum> baseList = new List<BaseUtility.BaseEnum>();
            foreach (BaseUtility.BaseEnum temp in Enum.GetValues(typeof(BaseUtility.BaseEnum)))
            {
                baseList.Add(temp);
            }

            return baseList;
        }
    }
}
