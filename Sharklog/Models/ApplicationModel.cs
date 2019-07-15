using System;

namespace sharklog.Models
{
    public class ApplicationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}