using Biblioteka.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Model
{
    public class CheckOut
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        [Required]
        public string BorrowerName { get; set; }

        [Required]
        [Phone]
        public string BorrowerPhone { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
