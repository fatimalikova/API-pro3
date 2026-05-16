using API_pro3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_pro3.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //fluent api ile konfigurasiya yaziriq
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(200);
            builder.Property(c => c.CreateDate).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.UpdateDate).IsRequired(false);
            
        }

      
    }
}
