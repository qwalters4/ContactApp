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

namespace ContactWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel MyViewModel { get { return (MainViewModel)DataContext; } }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.LoadContactList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyViewModel.Load();
            MyViewModel.FileName = "persistent.json";
            MyViewModel.ImportContactList();
            MyViewModel.FileName = "";
        }

        private void AddPhoneButton_Click(Object sender, RoutedEventArgs e)
        {
            MyViewModel.AddPhone();
            PhoneTypeText.Focus();
        }

        private void DeletePhoneButton_Click(Object sender, RoutedEventArgs e)
        {
            MyViewModel.DeletePhone();
        }

        private void AddContactButton_Click(Object sender, RoutedEventArgs e)
        {
            MyViewModel.AddContact();
        }

        private void DeleteContactButton_Click(Object sender, RoutedEventArgs e)
        {
            MyViewModel.DeleteContact();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.ImportContactList();
        }
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.ExportContactList();
        }

        private void EditNameButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.EditContactName();
        }

        private void SaveNameButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.SaveContactName();
        }

        private void CancelNameButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.CancelContactName();
        }

        private void DeleteEmailButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.DeleteEmail();
        }

        private void AddEmailButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.AddEmail();
            EmailTypeText.Focus();
        }

        private void DeleteAddressButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.DeleteAddress();
        }

        private void AddAddressButton_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.AddAddress();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MyViewModel.UpdateResults();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MyViewModel.FileName = "persistent.json";
            MyViewModel.ExportContactList();
        }

        private void AttachPhoto_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.UpdatePhoto();
        }

        private void ResetPhoto_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.ResetPhoto();
        }
    }
}
