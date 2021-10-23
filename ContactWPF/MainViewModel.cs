using ContactsLib;
using GalaSoft.MvvmLight;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ContactWPF
{
    class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Contact> m_Contacts;
        private ObservableCollection<Contact> m_SearchResults;
        private Visibility m_IntroVisibility;
        private Visibility m_ContactGridVisibility;
        private Visibility m_SaveCancelVisibility;
        private Visibility m_EditNameVisibility;
        private Contact m_SelectedContact;
        private Phone m_SelectedPhone;
        private Email m_SelectedEmail;
        private Address m_SelectedAddress;
        private string m_FileName;
        private string m_EditFirstName;
        private string m_EditLastName;
        private string m_EditCompany;
        private string m_SearchCriteria;


        public ObservableCollection<Contact> Contacts
        {
            get { return m_Contacts; }
            set
            {
                Set(ref m_Contacts, value);
            }
        }

        public ObservableCollection<Contact> SearchResults
        {
            get { return m_SearchResults; }
            set
            {
                Set(ref m_SearchResults, value);
            }
        }

        public string SearchCriteria
        {
            get { return m_SearchCriteria; }
            set { Set(ref m_SearchCriteria, value); }
        }

        public Visibility IntroVisibility
        {
            get { return m_IntroVisibility; }
            set { Set(ref m_IntroVisibility, value); }
        }

        public Visibility ContactGridVisibility
        {
            get { return m_ContactGridVisibility; }
            set { Set(ref m_ContactGridVisibility, value); }
        }

        public Visibility EditNameVisibility
        {
            get { return m_EditNameVisibility; }
            set { Set(ref m_EditNameVisibility, value); }
        }

        public Visibility SaveCancelVisibility
        {
            get { return m_SaveCancelVisibility; }
            set { Set(ref m_SaveCancelVisibility, value); }
        }

        public Contact SelectedContact
        {
            get { return m_SelectedContact; }
            set
            {
                if (Set(ref m_SelectedContact, value))
                {
                    SetContactGridVisibility();
                    EditNameVisibility = Visibility.Visible;
                    SaveCancelVisibility = Visibility.Collapsed;
                    SelectedPhone = null;
                }
            }
        }

        public Phone SelectedPhone
        {
            get { return m_SelectedPhone; }
            set { Set(ref m_SelectedPhone, value); }
        }

        public Email SelectedEmail
        {
            get { return m_SelectedEmail; }
            set { Set(ref m_SelectedEmail, value); }
        }

        public Address SelectedAddress
        {
            get { return m_SelectedAddress; }
            set { Set(ref m_SelectedAddress, value); }
        }

        internal void UpdateResults()
       {
            bool first, last;
            SearchResults.Clear();
            if (SearchCriteria == "")
            {
                foreach(Contact c in Contacts)
                {
                    SearchResults.Add(c);
                }
                return;
            }
            foreach (Contact c in Contacts)
            {
                first = c.FirstName.IndexOf(SearchCriteria, StringComparison.OrdinalIgnoreCase) >= 0;
                last = c.LastName.IndexOf(SearchCriteria, StringComparison.OrdinalIgnoreCase) >= 0;
                if (first || last)
                    SearchResults.Add(c);
            }
        }

        public void AddEmail()
        {
            if (SelectedContact == null) return;
            Email newemail = new Email()
            {
                Address = "<empty>"
            };
            SelectedContact.Emails.Add(newemail);
            SelectedEmail = newemail;
        }

        public void DeleteEmail()
        {
            if (SelectedContact == null) return;
            if (SelectedEmail == null) return;
            SelectedContact.Emails.Remove(SelectedEmail);
        }

        public string FileName
        {
            get { return m_FileName; }
            set { Set(ref m_FileName, value); }
        }

        public string EditFirstName
        {
            get { return m_EditFirstName; }
            set { Set(ref m_EditFirstName, value); }
        }

        public string EditLastName
        {
            get { return m_EditLastName; }
            set { Set(ref m_EditLastName, value); }
        }

        public string EditCompany
        {
            get { return m_EditCompany; }
            set { Set(ref m_EditCompany, value); }
        }

        public void Load()
        {
            DataService.Instance.ContactList = new ContactList();
            IntroVisibility = Visibility.Visible;
            ContactGridVisibility = Visibility.Visible;
            EditNameVisibility = Visibility.Visible;
            SaveCancelVisibility = Visibility.Collapsed;
            SearchCriteria = "";
            SetContactGridVisibility();
        }

        public void LoadContactList()
        {
            DataService.Instance.ContactList.LoadTestData();
            Contacts = DataService.Instance.ContactList.Contacts;
            SearchResults = new ObservableCollection<Contact>();
            foreach (Contact c in Contacts)
            {
                SearchResults.Add(c);
            }
            IntroVisibility = Visibility.Collapsed;
        }

        private void SetContactGridVisibility()
        {
            if (SelectedContact == null)
                ContactGridVisibility = Visibility.Visible;
            else
                ContactGridVisibility = Visibility.Collapsed;
        }

        public void AddPhone()
        {
            if (SelectedContact == null) return;
            Phone newphone = new Phone();
            SelectedContact.Phones.Add(newphone);
            SelectedPhone = newphone;
        }
        public void DeletePhone()
        {
            if (SelectedContact == null) return;
            if (SelectedPhone == null) return;
            SelectedContact.Phones.Remove(SelectedPhone);
        }

        public void AddAddress()
        {
            if (SelectedContact == null) return;
            Address newaddress = new Address();
            SelectedContact.Addresses.Add(newaddress);
            SelectedAddress = newaddress;
        }
        public void DeleteAddress()
        {
            if (SelectedContact == null) return;
            if (SelectedAddress == null) return;
            SelectedContact.Addresses.Remove(SelectedAddress);
        }

        public void AddContact()
        {
            Contact newContact = new Contact()
            {
                FirstName = "<First>",
                LastName = "<Last>",
            };
            Contacts.Add(newContact);
            SearchResults.Add(newContact);
        }

        public void DeleteContact()
        {
            if (SelectedContact == null) return;
            Contacts.Remove(SelectedContact);
            SearchResults.Remove(SelectedContact);
        }

        public void ImportContactList()
        {
            if (FileName == null)
                return;
            if (Contacts == null)
                Contacts = DataService.Instance.ContactList.Contacts;
            ContactList.Import(FileName, Contacts);
            IntroVisibility = Visibility.Collapsed;
            SearchResults = new ObservableCollection<Contact>();
            foreach (Contact c in Contacts)
            {
                SearchResults.Add(c);
            }
        }

        public void ExportContactList()
        {
            //System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            //dlg.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            //string selectedFolderName;

            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    selectedFolderName = dlg.SelectedPath;
            //}
            //else
            //    return;

            //if (Contacts == null)
            //    return;
            //ContactList.Export(selectedFolderName, Contacts);
            if (FileName == null)
                return;
            if (Contacts == null)
                return;
            ContactList.Export(FileName, Contacts);
        }

        public void EditContactName()
        {
            EditNameVisibility = Visibility.Collapsed;
            SaveCancelVisibility = Visibility.Visible;
            EditFirstName = SelectedContact.FirstName;
            EditLastName = SelectedContact.LastName;
            EditCompany = SelectedContact.Company;

        }

        public void CancelContactName()
        {
            EditNameVisibility = Visibility.Visible;
            SaveCancelVisibility = Visibility.Collapsed;
        }

        public void SaveContactName()
        {
            EditNameVisibility = Visibility.Visible;
            SaveCancelVisibility = Visibility.Collapsed;
            SelectedContact.FirstName = EditFirstName;
            SelectedContact.LastName = EditLastName;
            SelectedContact.Company = EditCompany;
        }

        public void UpdatePhoto()
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff";
            dlg.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|"
                + "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;

                SelectedContact.Photo.Location = selectedFileName;
            }
        }

        public void ResetPhoto()
        {
            string temp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            temp = Path.Combine(temp, "ContactApp/Portrait_Placeholder.png");
            SelectedContact.Photo.Location = temp;
        }
    }
}
