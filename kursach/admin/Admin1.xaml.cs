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
    /// Логика взаимодействия для Admin1.xaml
    /// </summary>
    public partial class Admin1 : Page
    {
        public Admin1()
        {
            InitializeComponent();
            DGridOpt.ItemsSource = OptEntities.GetContext().Supplies.ToList();
            UpdateClients();
        }
        private void UpdateClients()
        {
            var currentMedicines = OptEntities.GetContext().Supplies.ToList();

            currentMedicines = currentMedicines.Where(p => p.List_products.ToLower().Contains(TBoxSearch1.Text.ToLower())).ToList();
            DGridOpt.ItemsSource = currentMedicines.OrderBy(p => p.List_products).ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdit1((sender as Button).DataContext as Supplies));
        }

        private void TBoxSearch1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateClients();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdit1(null));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var DocumentComputersForRemoving = DGridOpt.SelectedItems.Cast<Supplies>().ToList();
            if (MessageBox.Show($"Вы точно хотите стереть этот {DocumentComputersForRemoving.Count()} файл без возврата?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    OptEntities.GetContext().Supplies.RemoveRange(DocumentComputersForRemoving);
                    OptEntities.GetContext().SaveChanges();
                    UpdateClients();
                    MessageBox.Show("Ну и ладненько!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ToString());
                    DGridOpt.ItemsSource = OptEntities.GetContext().Supplies.ToList();
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
                StreamWriter sw = new StreamWriter(new FileStream("Supplies.csv", FileMode.OpenOrCreate), Encoding.UTF32);
                sw.WriteLine(result);
                sw.Close();
                Process.Start("Supplies.csv");
            }
            catch (Exception)
            { }
        }
    }
}
