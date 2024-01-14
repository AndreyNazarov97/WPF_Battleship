using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Battleship
{
    abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T field, T value, [CallerMemberName] string propName = "") 
        {
            if(!field.Equals(value) || field == null) 
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }  
        }
        protected void Set<T>(ref T field, T value, params string[] propNames)
        {
            if (!field.Equals(value) || field == null)
            {
                field = value;
                foreach(var prop in propNames)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
                }
            }
        }
        protected void Notify(params string[] names)
        {
            foreach (var name in names)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}