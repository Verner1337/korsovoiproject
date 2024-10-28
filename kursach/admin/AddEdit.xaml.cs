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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace kursach.admin
{
    /// <summary>
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Page
    {
        private Product_stock _currentProduct_stock = new Product_stock();
        public AddEdit(Product_stock selectedProduct_stock)
        {
            InitializeComponent();
            if (selectedProduct_stock != null)
                _currentProduct_stock = selectedProduct_stock;

            DataContext = _currentProduct_stock;
        }
        private void BtnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(_currentProduct_stock.Product_name))
                    errors.AppendLine("Укажите название товара.");
                if (_currentProduct_stock.Quantity_product == null)
                    errors.AppendLine("Укажите количество.");
                if (_currentProduct_stock.Unit_measurement == null)
                    errors.AppendLine("Укажите объем.");
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                // тут идет проверка на ввод данных
                if (_currentProduct_stock.ID == 0)
                    OptEntities.GetContext().Product_stock.Add(_currentProduct_stock);
                try
                {
                    OptEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }
    }
}