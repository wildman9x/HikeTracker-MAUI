using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace HikeTracker.Model
{
    public class Observation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public int HikeID { get; set; }
        [Required]
        public string Name { get; set; }
        // Date and time to the minute
        [Required]
        public DateTime Date { get; set; }
        public string Comment { get; set; } = "";
    }
}