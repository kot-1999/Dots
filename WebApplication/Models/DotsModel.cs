using dots;
using dots.forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class DotsModel
    {
      
        public DotTable dotTable { get; set; }

        public List<InfoForm> infos { get; set; }
        public List<CommentForm> comments { get; set; }
    }
}
