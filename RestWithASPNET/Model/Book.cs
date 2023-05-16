using RestWithASPNETUdemy.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {

        [Column("Author")]
        public string Author { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("Title")]
        public string Title { get; set; }
    }
}
