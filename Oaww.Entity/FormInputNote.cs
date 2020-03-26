using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class FormInputNote
    {
        [ExplicitKey]
        public int InputID { get; set; }

        public string NoteText { get; set; }

        [ExplicitKey]
        public DateTime CreateDateTime { get; set; }

        public string Account { get; set; }

        public string Type { get; set; }

        public int? MainID { get; set; }

    }
}
