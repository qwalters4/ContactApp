using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsLib;

namespace ContactApp
{
    class Program
    {
        private static ContactList s_ContactList;
        static void Main(string[] args)
        {
            ContactManager mgr = new ContactManager();
            s_ContactList = mgr.LoadContactList();
            if(s_ContactList == null)
            {
                s_ContactList = mgr.CreateContactList();
            }

            Console.WriteLine("Enter command: ");
            string command = null;
            while ((command = Console.ReadLine()) != "quit") 
            {
                switch (command)
                {
                    case "count":
                        ProcessCount();
                        break;
                    case "load":
                        ProcessLoad();
                        break;
                    case "print":
                        ProcessPrint();
                        break;
                    case "export":
                        ProcessExport();
                        break;
                    case "import":
                        ProcessImport();
                        break;
                }
                Console.WriteLine("Enter command: ");
            }
        }

        private static void ProcessCount()
        {
            Console.WriteLine("The number of contacts in the list is " + s_ContactList.Contacts.Count);
        }

        private static void ProcessLoad()
        {
            s_ContactList.LoadTestData();
            ProcessCount();
        }

        private static void ProcessPrint()
        {
            s_ContactList.Print();
        }

        private static void ProcessExport()
        {
            string filename = null;
            while (filename == null || !File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename)))
            {
                Console.Write("Enter a filename(or type 'cancel'): ");
                filename = Console.ReadLine();
                if (filename == "cancel")
                {
                    return;
                }
            }
            s_ContactList.Export(filename);
        }

        private static void ProcessImport()
        {
            string filename = null;
            while (filename == null || !File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename)))
            {
                Console.Write("Enter a filename(or type 'cancel'): ");
                filename = Console.ReadLine();
                if (filename == "cancel")
                {
                    return;
                }
            }
            s_ContactList.Import(filename);
        }
    }
}
