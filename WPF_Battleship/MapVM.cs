using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Battleship
{
    class MapVM : ViewModelBase
    {
        CellVM[,] cellVM;

        public ObservableCollection<ShipVM> Ships { get; } = new ObservableCollection<ShipVM>();

        public CellVM this[int x, int y] => cellVM[y, x];
        public IReadOnlyCollection<IReadOnlyCollection<CellVM>> Map { get 
            {
                var map = new List<List<CellVM>>();
                for (int i = 0; i < 10; i++)
                {
                    map.Add(new List<CellVM>());
                    for (int j = 0; j < 10; j++)
                    {
                        map[i].Add(cellVM[i, j]);
                    }
                }
                return map;
            } 
        }

        public MapVM(string strmap)
        {
            var mp = strmap.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            cellVM = new CellVM[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cellVM[i,j] = new CellVM(mp[i][j]);
                }
            }
        }
        public MapVM()
        {
            cellVM = new CellVM[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cellVM[i, j] = new CellVM();
                }
            }
        }

        internal void SetShips(params ShipVM[] ships)
        {
            foreach(var ship in ships)
            {
                Ships.Add(ship);
                var (x, y) = ship.Pos;
                var rang = ship.Rang;
                var dir = ship.Direction;
                if(dir == ShipDirection.Horisontal)
                {
                    for(int j = x; j <= x + rang -1; j++)
                    {
                        this[j, y].PlaceShip();                  
                    }
                }
                else
                {
                    for (int i = y; i <= y + rang - 1; i++)
                    {
                        this[x, i].PlaceShip();
                    }
                }
            }
        }
    }
}
