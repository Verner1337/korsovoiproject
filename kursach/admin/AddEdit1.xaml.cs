using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace kursach.admin
{
    /// <summary>
    /// Логика взаимодействия для AddEdit1.xaml
    /// </summary>
    public partial class AddEdit1 : Page
    {
        private Supplies _currentSupplies = new Supplies();
        public AddEdit1(Supplies selectedSupplies)
        {
            InitializeComponent();
            if (selectedSupplies != null)
            { 
                _currentSupplies = selectedSupplies;
            }
            else
            {
                _currentSupplies.Delivery_date = DateTime.Now;
                _currentSupplies.ID = 1;
            }
                


            DataContext = _currentSupplies;
        }
        private void BtnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(_currentSupplies.List_products))
                    errors.AppendLine("Укажите название товара.");
                if (_currentSupplies.Delivery_date == null)
                    errors.AppendLine("Укажите дату доставки(с).");
                if (_currentSupplies.Quantity == null)
                    errors.AppendLine("Укажите цену(по).");
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                // тут идет проверка на ввод данных
                if (_currentSupplies.ID >= 0)
                    OptEntities.GetContext().Supplies.Add(_currentSupplies);
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
