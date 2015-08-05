namespace MavroImport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Beholder.ChapterMediaPublishedRel")]
    public partial class ChapterMediaPublishedRel
    {
        public int Id { get; set; }

        public int ChapterId { get; set; }

        public int MediaPublishedId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateStart { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateEnd { get; set; }

        public int RelationshipTypeId { get; set; }

        public int? ApprovalStatusId { get; set; }

        public int? PrimaryStatusId { get; set; }

        public virtual Chapter Chapter { get; set; }

        public virtual MediaPublished MediaPublished { get; set; }
    }
}
