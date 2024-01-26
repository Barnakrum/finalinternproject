using finalinternshipproject.Models;
using finalinternshipproject.Models.Fields;
using Microsoft.EntityFrameworkCore;

namespace finalinternshipproject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Tag> Tags { get; set; }


        public DbSet<BooleanField> BooleanField { get; set; }
        public DbSet<IntegerField> IntegerField { get; set; }
        public DbSet<StringField> StringField { get; set; }
        public DbSet<MultilineField> MultilineField { get; set; }
        public DbSet<DateTimeField> DateTimeField { get; set; }

        public DbSet<BooleanFieldName> BooleanFieldsNames { get; set; }
        public DbSet<IntegerFieldName> IntegerFieldsNames { get; set; }
        public DbSet<StringFieldName> StringFieldsNames { get; set; }
        public DbSet<MultilineFieldName> MultilineFieldsNames { get; set; }
        public DbSet<DateTimeFieldName> DateTimeFieldsNames { get; set; }





    }
}
