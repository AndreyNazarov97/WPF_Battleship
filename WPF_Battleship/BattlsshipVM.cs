﻿using System;
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

        public CellVM[][] OurMap {  get; set; }
        public CellVM[][] EnemyMap {  get; set; }
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

            OurMap = CreateMap();
            EnemyMap = CreateMap();
        }


        CellVM[][] CreateMap()
        {
            var map = new CellVM[10][];
            for (int i = 0; i < 10; i++)
            {
                map[i] = new CellVM[10];
                for (int j = 0; j < 10; j++)
                {
                    map[i][j] = new CellVM();
                }
            }
            return map;
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
    }
}