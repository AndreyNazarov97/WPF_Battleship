using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Battleship
{
    internal class CellVM : ViewModelBase
    {
        Visibility missVisibility = Visibility.Collapsed;
        public Visibility Miss 
        {
            get =>  missVisibility; 
            private set => Set(ref missVisibility, value); 
        }

        public void SetMiss()
        {
            Miss = Visibility.Visible;
        }
    }
}
