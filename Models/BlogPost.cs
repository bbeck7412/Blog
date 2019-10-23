using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Models
{
    public class BlogPost
    {   
        public int Id { get; set; }
        public DateTime Created { set; get; }
        public DateTime? Updated { get;set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Abstract { get; set; }
        [AllowHtml]
        public string BlogPostBody { get; set; }  
        public string ImageLocation { get; set; }
        public bool Published { get; set; }

        // Navigation
        public virtual ICollection<Comment> Comments { get; set; }
        public BlogPost ()
        {
            this.Comments = new HashSet<Comment>();
        }

    }
}