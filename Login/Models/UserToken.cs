using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApiService.Models
{
    public class UserToken
    {
        [Key]
        public int TokenId {get; set;}

        [Required]
        public string Token { get; set; }

        [ForeignKey("User")]
        public int UserRecId { get; set; }

        public DateTime Expires { get; set; }

        public DateTime Created { get; set; }

        public virtual User User { get; set; }
    }
}
