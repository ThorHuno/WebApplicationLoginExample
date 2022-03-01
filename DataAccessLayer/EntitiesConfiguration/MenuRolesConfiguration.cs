using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntitiesConfiguration
{
    internal class MenuRolesConfiguration : IEntityTypeConfiguration<MenuRoles>
    {
        public void Configure(EntityTypeBuilder<MenuRoles> builder)
        {
            builder.ToTable("MenuRoles");
            builder.HasKey("MenuId", "RoleId");
        }
    }
}
