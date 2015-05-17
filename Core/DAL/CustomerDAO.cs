namespace RoundTheClock.Core.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customers")]
    public partial class CustomerDAO
    {
        public CustomerDAO()
        {
            Entries = new HashSet<EntryDAO>();
            Projects = new HashSet<ProjectDAO>();
        }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id { get; set; }

        public virtual ICollection<EntryDAO> Entries { get; set; }

        public virtual ICollection<ProjectDAO> Projects { get; set; }
    }
}
