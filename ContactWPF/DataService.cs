using ContactsLib;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactWPF
{
    class DataService: ObservableObject
    {
        private static DataService s_Instance;

        private ContactList m_ContactList;

        static DataService()
        {
            s_Instance = new DataService();
        }

        public static DataService Instance {  get { return s_Instance; } }

        public ContactList ContactList
        {
            get { return m_ContactList; }
            set { Set(ref m_ContactList, value); }
        }
    }
}
