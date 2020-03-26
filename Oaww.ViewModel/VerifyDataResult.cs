using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.ViewModel
{
    public class VerifyDataResult
    {
        public string UpdateTime { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateStatus { get; set; }
        public string Status { get; set; }
        public string OptionHtml { get; set; }
        public string TitleLink { get; set; }
        [Write(false)]
        public string ModelName { get; set; }
    }
}
