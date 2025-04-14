using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.DAL.Model
{
    public class Comment
 
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Post")]
        [Required]
        public long PostId { get; set; }
        [Required]
        public string Content { get; set; }
    }

}
