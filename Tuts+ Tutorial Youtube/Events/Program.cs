using System;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var tower = new ClockTower();
            var john = new Person("John", tower);

            tower.ChimeFivePM();
        }
    }

    public class Person
    {
        private string _name;
        private ClockTower _tower;

        public Person(string name, ClockTower tower)
        {
            _name = name;
            _tower = tower;

            _tower.Chime += (object sender, ClockTowerEventArgs args) => {
                System.Console.WriteLine("{0} heard the clock chime", _name);
                switch (args.Time) {
                    case 6: System.Console.WriteLine("{0} is waking up", _name);
                        break;
                    case 17: System.Console.WriteLine("{0} is going home", _name);
                        break;
                }
            };
        }
    }

    public class ClockTowerEventArgs : EventArgs{
        public int Time { get; set; }
    }

    //The object that we want to watch/ subscribe to
    public delegate void ChimeEventHandler(object sender, ClockTowerEventArgs args);
    public class ClockTower
    {
        //an event type is just a glorified delegate type, the declaration is similar to that of a delegate, and the event return type is always a delegate, in which case, turns out to be ChimeEventHandler
        public event ChimeEventHandler Chime;
        public void ChimeFivePM(){
            Chime(this, new ClockTowerEventArgs{Time = 17});
        }

        public void ChimeSixAM(){
            Chime(this, new ClockTowerEventArgs{Time = 16});
        }
    }
}
