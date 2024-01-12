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
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
            
        }
        protected void Fire(params string[] names)
        {
            foreach (var name in names)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}