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

        bool ship, shoot;

        public CellVM(char state = '*')
        {
            ship = state == 'X';
        }

        public Visibility Miss 
        {
            get =>  shoot && !ship ? Visibility.Visible : Visibility.Collapsed; 
        }
        public Visibility Hit
        {
            get => shoot && ship ? Visibility.Visible : Visibility.Collapsed;
        }
        
        public void ToShoot()
        {
            shoot = true;
            Notify("Miss", "Hit");
        }

        public void PlaceShip()
        {
            ship = true;
        }
    }
}
