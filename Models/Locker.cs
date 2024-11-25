using SQLite;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace SzafkiSzkolne.Models
{
    public class Locker
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int LockerNr { get; set; }
        public string RegalNr { get; set; }
        public string Owner { get; set; }
        public int Floor { get; set; }
        public bool isOccupied { get; set; }

        public string isOccupiedText
        {
            get => isOccupied ? "Tak" : "Nie";
            set
            {
                if (value == "Tak") isOccupied = true;
                else if (value == "Nie") isOccupied = false;
            }
        }
    }
}
