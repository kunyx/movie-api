using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models
{
    public class Stats
    {
        [Key]
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int WatchDurationMs { get; set; }
    }
}
