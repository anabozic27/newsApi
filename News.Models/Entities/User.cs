using System;
using System.Collections.Generic;

namespace News.Models.Entities
{
    public partial class User
    {
        public User()
        {
            ArticleCreateByUser = new HashSet<Article>();
            ArticleModifiedByUser = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? ArticleCategoryId { get; set; }
        public bool Active { get; set; }

        public virtual Category ArticleCategory { get; set; }
        public virtual ICollection<Article> ArticleCreateByUser { get; set; }
        public virtual ICollection<Article> ArticleModifiedByUser { get; set; }
    }
}
