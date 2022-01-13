using System.Collections.Generic;

namespace News.Models.Entities
{
    public partial class Category
    {
        public Category()
        {
            Article = new HashSet<Article>();
            InverseParentCategory = new HashSet<Category>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
