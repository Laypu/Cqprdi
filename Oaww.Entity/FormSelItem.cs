using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class FormSelItem
    {
        [Key]
        public int ID { get; set; }

        public int? ItemID { get; set; }

        public int? Sort { get; set; }

        public bool MustInput { get; set; }

        public int? TextLength { get; set; }

        public int? ColumnNum { get; set; }

        public int? RowNum { get; set; }

        public int? ItemMode { get; set; }

        public string Title { get; set; }

        public string DefaultText { get; set; }

        public string Description { get; set; }

        public string SelList { get; set; }

        public Boolean? Status { get; set; }

        public string KayName { get; set; }

    }
}
