using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Model
{
    [Table("CONTACT_INFO")]
    public partial class ContactInfo
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [Column("SENDER_EMAIL")]
        [StringLength(20)]
        public string SenderEmail { get; set; }
        [Required]
        [Column("SENDER_NAME")]
        [StringLength(50)]
        public string SenderName { get; set; }
        [Required]
        [Column("SENDER_PHONE")]
        [StringLength(20)]
        public string SenderPhone { get; set; }
        [Required]
        [Column("SENDER_MESSAGE")]
        [StringLength(200)]
        public string SenderMessage { get; set; }
        [Column("LAST_UPDATED_TIME", TypeName = "datetime2(3)")]
        public DateTime LastUpdatedTime { get; set; }
    }
}
