using SQLite;

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
        public string isOccupied { get; set; }

    }
}
