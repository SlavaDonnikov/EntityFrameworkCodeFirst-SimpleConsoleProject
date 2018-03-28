using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstDatabaseNewDatabaseSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create and save new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };
                db.Blogs.Add(blog);
                db.SaveChanges();

                // Display all Blogs from the database

                var query = from b in db.Blogs orderby b.Name select b;
                Console.WriteLine("All blogs in the database: ");
                foreach (var item in query) Console.WriteLine(item.Name);

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }

    #region class Blog & class Post with SQL attributes
    /// <summary>
    /// We making 2 navigation properties (Blog.Post & Post.Blog) virtual.
    /// This enables the Lazy Loading feature of Entity Framework.
    /// Lazy Loading means that the contents of these properties will be
    /// automatically loaded from the database when you try to access them.    /// 
    /// </summary>
    [Table("Tb_Blog")]
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR(MAX)")]
        [Required]
        public string Url { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    [Table("Tb_Post")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        [Required]
        public string Content { get; set; }

        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    [Table("Tb_User")]
    public class User
    {
        [Key]
        public string Username { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        [Required]
        public string DisplayName { get; set; }
    }
    #endregion

    #region BloggingContext : DbContext
    /// <summary>
    /// Now I define a derived context, which represents a session with the database,
    /// allowing us to query and save data. 
    /// I define a context that derives from "System.Data.Entuty.DbContext" 
    /// and exposes a typed "DbSet<TEntity>" for each class in model.
    /// </summary>
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        //Access the fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.DisplayName).HasColumnName("DisplayName"); 
        }
    }
    #endregion
}
