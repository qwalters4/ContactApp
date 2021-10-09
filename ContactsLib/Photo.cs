using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLib
{
    public class Photo: ObjectBase
    {
        private string m_Location;
        
        public string Location
        {
            get { return m_Location; }
            set { Set(ref m_Location, value, nameof(Location)); }
        }

        public Photo()
        {
            string temp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            temp = Path.Combine(temp, "ContactApp/Portrait_Placeholder.png");
            m_Location = temp;
        }
    }
}
