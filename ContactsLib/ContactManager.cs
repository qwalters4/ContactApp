using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLib
{
    public class ContactManager
    {
        public ContactList CreateContactList()
        {
            return new ContactList();
        }

        public ContactList LoadContactList()
        {
            ContactList temp;
            temp = new ContactList();
            temp.Import("persistent.json");

            return temp;
        }
    }
}
