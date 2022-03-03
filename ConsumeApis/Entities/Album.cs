using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeApis.Entities
{
    public class Album:BaseEntity
    {
        public int userId { get; set; }
        public string title { get; set; }
    }
}
