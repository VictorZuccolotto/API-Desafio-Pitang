using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioPitang.Entities.Entities;

namespace DesafioPitang.Repository.Map
{
    public class AgendamentoMap : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("tb_agendamento");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("id_agendamento")
                   .IsRequired();

            builder.Property(e => e.PacienteId)
                   .HasColumnName("id_paciente")
                   .IsRequired();

            builder.Property(e => e.DataAgendamento)
                   .HasColumnName("dat_agendamento")
                   .IsRequired();

            builder.Property(e => e.HoraAgendamento)
                   .HasColumnName("hor_agendamento")
                   .IsRequired();
            
            builder.Property(e => e.Status)
                   .HasColumnName("dsc_status")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.DataCriacao)
                   .HasColumnName("dat_criacao")
                   .IsRequired();

            builder.HasOne(a => a.Paciente)
                   .WithMany(p => p.Agendamentos)
                   .HasForeignKey(a => a.PacienteId);
        }
    }
}
