using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Seat
    {
        [Key]
        public int? seat_id { get; set; }
        public int? seat_total { get; set; }
        public string? Flag { get; set; }
        public int? seat_booked { get; set; }
        public string? seat_status {
            get
            {
                if (seat_booked < seat_total)
                    return "On Seat";
                else if (seat_booked == seat_total)
                    return "Disabled";
                else
                    return "Unknown";
            } 
        }
        public int? total_economy { get; set; }
        public int? total_business { get; set; }
        public int? economy_booked { get; set; }
        public int? business_booked { get; set; }
        public string? economy_status
        {
            get
            {
                if (economy_booked < total_economy)
                    return "On Seat";
                else if (economy_booked == total_economy)
                    return "Disabled";
                else
                    return "Unknown";
            }
        }
        public string? business_status
        {
            get
            {
                if (business_booked < total_business)
                    return "On Seat";
                else if (business_booked == total_business)
                    return "Disabled";
                else
                    return "Unknown";
            }
        }

    }
}
