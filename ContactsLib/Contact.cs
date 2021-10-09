using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLib
{
    public class Contact: ObjectBase
    {
        private string m_FirstName;
        private string m_LastName;
        private string m_Company;
        private ObservableCollection<Phone> m_Phones;
        private ObservableCollection<Email> m_Emails;
        private ObservableCollection<Address> m_Addresses;
        private Photo m_Photo;

        public string FirstName
        {
            get { return m_FirstName; }
            set 
            { 
                Set(ref m_FirstName, value, nameof(FirstName));
                DoPropertyChanged(nameof(FirstName));
                DoPropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get { return m_LastName; }
            set 
            { 
                Set(ref m_LastName, value, nameof(LastName));
                DoPropertyChanged(nameof(LastName));
                DoPropertyChanged(nameof(FullName));
            }
        }

        public string Company
        {
            get { return m_Company; }
            set { Set(ref m_Company, value, nameof(Company)); }
        }

        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        public ObservableCollection<Phone> Phones { get { return m_Phones; } }
        public ObservableCollection<Email> Emails { get { return m_Emails; } }
        public ObservableCollection<Address> Addresses { get { return m_Addresses; } }

        public Photo Photo
        {
            get { return m_Photo; }
            set 
            { 
                Set(ref m_Photo, value, nameof(Photo));
                DoPropertyChanged(nameof(Photo));
            }
        }

        public Contact()
        {
            m_Phones = new ObservableCollection<Phone>();
            m_Emails = new ObservableCollection<Email>();
            m_Addresses = new ObservableCollection<Address>();
        }
    }
}
