using System;
using System.Collections.Generic;

namespace News.Models.Entities
{
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public int CreateByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }

        public virtual Category Category { get; set; }
        public virtual User CreateByUser { get; set; }
        public virtual User ModifiedByUser { get; set; }
    }
}
