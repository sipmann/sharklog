using System;
using System.ComponentModel.DataAnnotations;

namespace sharklog.Models
{
    public class LogModel
    {
        [StringLength(40)]
        public string Id { get; set; }

        public ApplicationModel Application { get; set; }

        public string Title { get; set; }

        public string LogType { get; set; }

        public DateTime LogDate { get; set; }

        public string Body { get; set; }
    }
}