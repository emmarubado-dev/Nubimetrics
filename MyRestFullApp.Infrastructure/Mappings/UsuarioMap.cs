using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestFullApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestFullApp.Infrastructure.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(c => c.UserName);
            builder.Property(c => c.Password).IsRequired();
            builder.Property(c => c.Nombre).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Apellido).IsRequired().HasMaxLength(200);
        }
    }
}