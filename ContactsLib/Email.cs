using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLib
{
    public class Email: ObjectBase
    {
        private string m_Address;
        private string m_Type;

        public string Address
        {
            get { return m_Address; }
            set { Set(ref m_Address, value, nameof(Address)); }
        }

        public string Type
        {
            get { return m_Type; }
            set { Set(ref m_Type, value, nameof(Type));}
        }
    }
}
