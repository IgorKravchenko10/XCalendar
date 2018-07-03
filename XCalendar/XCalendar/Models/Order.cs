using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCalendar.Models
{
    [Table("Orders")]
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        private TimeSpan _time;
        [Ignore]
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
                this.TimeString = _time.ToString(@"hh\:mm");
            }
        }
        
        public string TimeString { get; private set; }

    }
}
