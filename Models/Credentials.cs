using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    [Table("credentials", Schema = "auth")]
    public class Credential
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Email { get; set; } = null;

        [Column("login_phone")]
        [MaxLength(10)]
        public string LoginPhone { get; set; } = null;

        [Column("hashed_password")]
        [Required]
        [MaxLength(225)]
        public string HashedPassword { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("login_at")]
        public DateTime? LoginAt { get; set; }
    }
}