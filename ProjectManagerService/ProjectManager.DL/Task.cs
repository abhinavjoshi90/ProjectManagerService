namespace ProjectManager.DL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task")]
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            Usrs = new HashSet<Usr>();
        }

        [Key]
        public int Task_ID { get; set; }

        public int? Parent_ID { get; set; }

        public int? Project_ID { get; set; }

        [StringLength(50)]
        public string Task_Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public int? Priority { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public virtual ParentTask ParentTask { get; set; }

        public virtual Project Project { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usr> Usrs { get; set; }
    }
}
