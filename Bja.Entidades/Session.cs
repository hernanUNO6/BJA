using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class Session
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public User User { get; set; }
        public String UserName { get; set; }
        public String CompleteName { get; set; }
        public DateTime InitDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
