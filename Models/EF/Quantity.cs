namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quantity")]
    public partial class Quantity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        [Column("Quantity")]
        public int? Quantity1 { get; set; }

        [StringLength(50)]
        public string Size { get; set; }
    }
}
