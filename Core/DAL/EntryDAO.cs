namespace RoundTheClock.Core.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entries")]
    public partial class EntryDAO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long project_fk { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long task_fk { get; set; }

        [Key]
        [Column("from", Order = 2)]
        public string From { get; set; }

        [Key]
        [Column("date", Order = 3)]
        public string Date { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long customer_fk { get; set; }

        [Key]
        [Column("to", Order = 5)]
        public string To { get; set; }

        public virtual CustomerDAO Customers { get; set; }

        public virtual ProjectDAO Projects { get; set; }

        public virtual TaskDAO Tasks { get; set; }
    }
}
