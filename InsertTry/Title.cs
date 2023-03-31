using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertTry
{
    public class Title
    {
        [Key]
        public string tconst { get; set; }
        public string titleType { get; set; }
        public string primaryTitle { get; set; }
        public string originalTitle { get; set; }
        public bool? isAdult { get; set; }
        public int? startYear { get; set; }
        public int? endYear { get; set; }
        public int? runtimeMinute { get; set; }
        public string genre { get; set; }
    }
}
