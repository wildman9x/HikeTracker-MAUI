using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace HikeTracker.Model
{
    public class Hike
    {
        private DateTime _date;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public double Length { get; set; }
        [Required]
        public DateTime Date
        {
            // Return only date portion of DateTime
            get => _date.Date;
            set => _date = value;
        }
        [Required]
        public bool ParkingAvailable { get; set; }
        [Required]
        public string Difficulty { get; set; }
        public string Description { get; set; }
        [Required]
        public string Weather { get; set; }
        [Required]
        public bool HasWaterFountain { get; set; }
    }
}