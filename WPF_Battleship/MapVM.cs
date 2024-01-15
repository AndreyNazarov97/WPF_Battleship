using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace WPF_Battleship
{
    class MapVM : ViewModelBase
    {
        CellVM[,] cellVM;
        static Random rnd = new Random();

        public ObservableCollection<ShipVM> Ships { get; } = new ObservableCollection<ShipVM>();

        public CellVM this[int x, int y] => cellVM[y, x];
        public IReadOnlyCollection<IReadOnlyCollection<CellVM>> Map { get
            {
                var viewMap = new List<List<CellVM>>();
                for (int i = 0; i < 10; i++)
                {
                    viewMap.Add(new List<CellVM>());
                    for (int j = 0; j < 10; j++)
                    {
                        viewMap[i].Add(cellVM[i, j]);
                    }
                }
                return viewMap;
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
                    cellVM[i, j] = new CellVM(mp[i][j]);
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

        // FillMap(0,4,3,2,1);
        private List<Ship> fillMap(List<Ship> ships, params int[] fleet)
        {
            var p = 0;
            while (p < fleet.Length && fleet[p] == 0) p++;
            if (p == fleet.Length)
            {
                return ships;
            }
            else
            {
                var ship = new Ship();
                ship.Rang = p;
                fleet[p]--;
                int k = 0;
                while (k < 50)
                {
                    if (rnd.Next(2) == 0)
                    {
                        ship.Dir = ShipDirection.Horisontal;
                        ship.X = rnd.Next(11 - p);
                        ship.Y = rnd.Next(10);
                    }
                    else
                    {
                        ship.Dir = ShipDirection.Vertical;
                        ship.X = rnd.Next(10);
                        ship.Y = rnd.Next(11 - p);
                    }
                    
                    if (ships.All(other => !ship.Cross(ref other)))
                    {
                        ships.Add(ship);


                        var res = fillMap(ships, fleet);
                        if (res != null)
                        {
                            return res;
                        }
                        ships.RemoveAt(ships.Count - 1);
                    }
                }
                fleet[p]++;
            }
            return null;
        }

        public void FillMap(params int[] fleet)
        {
            List<Ship> ships = null;
            while(ships == null)
            {
                ships = fillMap(new List<Ship>(), fleet);
            }
            foreach(var ship in ships)
            {
               if(ship.Dir == ShipDirection.Horisontal)
                {
                    for(int x = ship.X; x <= ship.X + ship.Rang - 1; x++)
                    {
                        cellVM[x, ship.Y].PlaceShip();
                    }
                }
                else
                {
                    for (int y = ship.Y; y <= ship.Y + ship.Rang - 1; y++)
                    {
                        cellVM[ship.X, y].PlaceShip();
                    }
                }
            }
        }
        private struct Ship
        {
            public int X=0, Y=0, Rang=0;
            public ShipDirection Dir =0;

            public Ship() { }
            public Ship(int x, int y, int rang, ShipDirection dir)
            {
                X = x; Y = y; Rang = rang; Dir = dir;
            }             


            //Переделать метод
            public bool Cross(ref Ship other)
            {
                int x = X, y = Y, xx = X, yy = Y;
                if(Dir == ShipDirection.Horisontal)
                {
                    xx = x + Rang - 1;
                }
                else
                {
                    yy = y + Rang - 1;
                }
                var ox = other.X;
                var oy = other.Y;
                int oxx = ox, oyy = oy;
                if (Dir == ShipDirection.Horisontal)
                {
                    oxx += other.Rang - 1;
                }
                else
                {
                    oyy += other.Rang - 1;
                }
                return x <= ox && ox <= xx && y <= oy && oy <= yy ||
                        x<= oxx & oxx <= xx && y<= oyy && oyy <= yy;
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
