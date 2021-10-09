using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLib
{
    public class ObjectBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void DoPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected void Set<T>(ref T oldValue, T newValue, string propName)
        {
            if (!Object.Equals(oldValue, newValue))
            {
                oldValue = newValue;
                DoPropertyChanged(propName);
            }
        }
    }
}
