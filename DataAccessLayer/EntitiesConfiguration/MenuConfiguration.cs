using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntitiesConfiguration
{
    internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn(1, 1).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Url).IsRequired().HasMaxLength(250);

            builder.HasMany(p => p.MenuRoles).WithOne(p => p.Menu).HasForeignKey(p => p.MenuId).IsRequired();
            builder.HasOne(p => p.ParentMenu).WithMany(p => p.Submenus).HasForeignKey(p => p.SubMenuId).IsRequired(false);
        }
    }
}
