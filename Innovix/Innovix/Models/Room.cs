using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Innovix.Models
{
    public class Room
    {
        [Key]
        public int RoomNo { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public virtual RoomType Type { get; set; }

    }
}
