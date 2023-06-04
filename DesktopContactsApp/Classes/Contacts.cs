using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopContactsApp.Classes
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Mark Id as primary key and auto increment attribute
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        // Override the ToString methode
        public override string ToString()
        {
            return $"{Id}: {Name}, {Email}, {Phone}";
        }
    }
}

