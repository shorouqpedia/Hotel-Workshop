using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Innovix.Models
{
    public class SeasonType
    {
        public int Id { get; set; }
        [ForeignKey("Type")]
        public int typeId { get; set; }
        [ForeignKey("Season")]
        public int SeasonId { get; set; }
        public decimal Rate { get; set; }
        public virtual RoomType Type { get; set; }
        public virtual Season Season { get; set; }

    }
}
