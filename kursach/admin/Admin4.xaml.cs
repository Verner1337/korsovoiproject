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
    /// Логика взаимодействия для Admin4.xaml
    /// </summary>
    public partial class Admin4 : Page
    {
        public Admin4()
        {
            InitializeComponent();
            DGridOpt.ItemsSource = OptEntities.GetContext().Sales.ToList();
            UpdateClients();
        }
        private void UpdateClients()
        {
            var currentMedicines = OptEntities.GetContext().Sales.ToList();

            currentMedicines = currentMedicines.Where(p => p.List_products.ToLower().Contains(TBoxSearch1.Text.ToLower())).ToList();
            DGridOpt.ItemsSource = currentMedicines.OrderBy(p => p.List_products).ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdit(null));
        }

        private void TBoxSearch1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateClients();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdit(null));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var DocumentsForRemoving = DGridOpt.SelectedItems.Cast<Sales>().ToList();
            if (MessageBox.Show($"Вы точно хотите стереть этот {DocumentsForRemoving.Count()} файл без возврата?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    OptEntities.GetContext().Sales.RemoveRange(DocumentsForRemoving);
                    OptEntities.GetContext().SaveChanges();
                    UpdateClients();
                    MessageBox.Show("Ну и ладненько!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ToString());
                    DGridOpt.ItemsSource = OptEntities.GetContext().Sales.ToList();
                }
            }
        }

        private void BtnForm_Click(object sender, RoutedEventArgs e)
        {
            this.DGridOpt.SelectAllCells();
            this.DGridOpt.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.DGridOpt);
            this.DGridOpt.UnselectAllCells();
            String result = Clipboard.GetData(DataFormats.CommaSeparatedValue).ToString();
            try
            {
                //StreamWriter sw = new StreamWriter("Documents.csv");
                StreamWriter sw = new StreamWriter(new FileStream("Sales.csv", FileMode.OpenOrCreate), Encoding.UTF32);
                sw.WriteLine(result);
                sw.Close();
                Process.Start("Sales.csv");
            }
            catch (Exception)
            { }
        }
    }
}
