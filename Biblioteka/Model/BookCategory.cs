using Biblioteka.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Model
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
