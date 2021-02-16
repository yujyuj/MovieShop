using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations; //[MaxLength(24)]
using System.ComponentModel.DataAnnotations.Schema; //[Talbe("Genre")]

namespace MovieShop.Core.Entities
{
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(24)]
        public string Name { get; set; }
    }
}
