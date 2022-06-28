using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApiService.Models
{
    public class User
    {
        [Key]
        public int UserRecId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string EmailId { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<UserToken> UserToken { get; set; }
    }
}
