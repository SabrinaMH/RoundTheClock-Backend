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
        public EntryDAO()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long project_fk { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long task_fk { get; set; }

        [Column("from")]
        public string From { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long customer_fk { get; set; }

        [Column("to")]
        public string To { get; set; }

        [Column("committed")]
        public bool Committed { get; set; }

        public virtual CustomerDAO Customer { get; set; }

        public virtual ProjectDAO Project { get; set; }

        public virtual TaskDAO Task { get; set; }
    }
}
