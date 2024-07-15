using DesafioPitang.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Repository.Map
{
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("tb_paciente");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("id_paciente")
                   .IsRequired();

            builder.Property(e => e.Nome)
                   .HasColumnName("dsc_nome")
                   .IsRequired();

            builder.Property(e => e.DataNascimento)
                   .HasColumnName("dat_nasicmento")
                   .IsRequired();

            builder.Property(e => e.DataCriacao)
                   .HasColumnName("dat_criacao")
                   .IsRequired();
        }
    }
}
