using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WPF_Battleship
{
    class BattlsshipVM : ViewModelBase
    {
        DispatcherTimer timer;
        DateTime startTime;
        string time = "";

        string enemyMap =
            @"
***********
***********
***********
*XXXXXXXX*
***********
***********
***********
***********
***********
***********
";

        public MapVM OurMap {  get; set; }
        public MapVM EnemyMap {  get; set; }
        public string Time 
        { 
            get => time;
            private set => Set(ref time, value);
        }
        public BattlsshipVM()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;

            OurMap = new MapVM();
            OurMap.SetShips(
                new ShipVM(4, (1,1)),
                new ShipVM(3, (8,1), ShipDirection.Vertical),
                new ShipVM(3, (6,1), ShipDirection.Vertical),
                new ShipVM(2, (3,5), ShipDirection.Vertical),
                new ShipVM(2, (7, 5)),
                new ShipVM(2, (8, 7)),
                new ShipVM(1, (0, 7)),
                new ShipVM(1, (2, 9)),
                new ShipVM(1, (9, 9)),
                new ShipVM(1, (4, 9))
                );
            EnemyMap = new MapVM(enemyMap);

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var now = DateTime.Now;
            var dt = now - startTime;
            Time = dt.ToString(@"mm\:ss");
            
        }

        public void Start()
        {
            startTime = DateTime.Now;
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
        }

        internal void EnemyHit(int x, int y)
        {
            OurMap[x,y].ToShoot();
        }
    }
}
