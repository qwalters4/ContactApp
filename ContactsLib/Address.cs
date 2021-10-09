using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLib
{
    public class Address: ObjectBase
    {
        private string m_Street;
        private string m_City;
        private string m_State;
        private string m_PostalCode;
        private string m_Country;

        public string Street
        {
            get { return m_Street; }
            set
            { 
                Set(ref m_Street, value, nameof(Street));
                DoPropertyChanged(nameof(Street));
                DoPropertyChanged(nameof(FormattedAddress));
            }
        }

        public string City
        {
            get { return m_City; }
            set
            { 
                Set(ref m_City, value, nameof(City));
                DoPropertyChanged(nameof(City));
                DoPropertyChanged(nameof(FormattedAddress));
            }
        }

        public string State
        {
            get { return m_State; }
            set 
            { 
                Set(ref m_State, value, nameof(State));
                DoPropertyChanged(nameof(State));
                DoPropertyChanged(nameof(FormattedAddress));
            }
        }

        public string PostalCode
        {
            get { return m_PostalCode; }
            set
            { 
                Set(ref m_PostalCode, value, nameof(PostalCode));
                DoPropertyChanged(nameof(PostalCode));
                DoPropertyChanged(nameof(FormattedAddress));
            }
        }

        public string Country
        {
            get { return m_Country; }
            set
            { 
                Set(ref m_Country, value, nameof(Country));
                DoPropertyChanged(nameof(Country));
                DoPropertyChanged(nameof(FormattedAddress));
            }
        }

        public string FormattedAddress { get { return this.ToString(); } }

        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(Street) &&
                String.IsNullOrWhiteSpace(City) &&
                String.IsNullOrWhiteSpace(State) &&
                String.IsNullOrWhiteSpace(PostalCode) &&
                String.IsNullOrWhiteSpace(Country))
                return "<empty>";
            return Street + ", " + City + ", " + State + ", "  + Country;
        }
    }
}
