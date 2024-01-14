using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_Battleship
{
	
	enum ShipDirection
	{
		Horisontal,
		Vertical
	}
    class ShipVM : ViewModelBase
    {

		private int rang = 1;
        
		public int Rang
		{
			get => rang;
			set => Set(ref rang, value, "RangView");
		}
		public int RangView => Rang * App.CellSize - 5;

        ShipDirection dir = ShipDirection.Horisontal;
		public ShipDirection Direction
		{
			get => dir;
			set => Set(ref dir, value, "Angle");
		}
        public int Angle => dir == ShipDirection.Horisontal ? 0 : 90;

		private int x;
		public int X => pos.x * App.CellSize +3;

        private int y;
        public int Y => pos.y * App.CellSize +3;

		private (int x, int y) pos;
		public (int,int) Pos 
		{ 
			get => pos;
			set => Set(ref pos, value, "X", "Y");
		}
        public ShipVM(int rang, (int,int) pos, ShipDirection dir = ShipDirection.Horisontal)
        {
			Rang = rang;	
			Pos = pos;
			Direction = dir;
        }
        public ShipVM()
        {
            
        }
    }
}
