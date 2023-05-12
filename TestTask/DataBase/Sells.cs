namespace TestTask.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sells
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? ID_Manager { get; set; }

        public int? ID_Products { get; set; }

        public int? Count { get; set; }

        public decimal? Sum { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public virtual Managers Managers { get; set; }

        public virtual Products Products { get; set; }
    }
}
