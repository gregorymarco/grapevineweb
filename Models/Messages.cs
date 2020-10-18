using System;
namespace website.Models
{
    public partial class Messages
    {
        public int Id { get; set; }
        public string Messagecontent { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public DateTime? Heure { get; set; }
    }
}
