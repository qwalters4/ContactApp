using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLib
{
    public class Phone: ObjectBase
    {
        private string m_AreaCode;
        private string m_Number;
        private string m_Extension;
        private string m_Type;

        public string AreaCode
        {
            get { return m_AreaCode; }
            set 
            { 
                Set(ref m_AreaCode, value, nameof(AreaCode));
                DoPropertyChanged(nameof(FormattedPhone));
            }
        }

        public string Number
        {
            get { return m_Number; }
            set 
            { 
                Set(ref m_Number, value, nameof(Number));
                DoPropertyChanged(nameof(FormattedPhone));
            }
        }

        public string Extension
        {
            get { return m_Extension; }
            set 
            { 
                Set(ref m_Extension, value, nameof(Extension));
                DoPropertyChanged(nameof(FormattedPhone));
            }
        }

        public string Type
        {
            get { return m_Type; }
            set 
            { 
                Set(ref m_Type, value, nameof(Type));
                DoPropertyChanged(nameof(FormattedPhone));
            }
        }

        public string FormattedPhone { get { return this.ToString(); } }

        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(AreaCode) &&
                String.IsNullOrWhiteSpace(Number) &&
                String.IsNullOrWhiteSpace(Extension) &&
                String.IsNullOrWhiteSpace(Type))
                return "<empty>";
            if (String.IsNullOrWhiteSpace(AreaCode))
                return Type + ": " + Number + " ext. " + Extension;
            return Type + ": (" + AreaCode + ")" + Number + " ext. " + Extension;
        }
    }
}
