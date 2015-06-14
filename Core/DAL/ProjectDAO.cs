namespace RoundTheClock.Core.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Projects")]
    public partial class ProjectDAO
    {
        public ProjectDAO()
        {
            Entries = new HashSet<EntryDAO>();
            Customers = new HashSet<CustomerDAO>();
            Tasks = new HashSet<TaskDAO>();
        }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public virtual ICollection<EntryDAO> Entries { get; set; }

        public virtual ICollection<CustomerDAO> Customers { get; set; }

        public virtual ICollection<TaskDAO> Tasks { get; set; }
    }
}
