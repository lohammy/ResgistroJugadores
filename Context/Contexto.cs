using Microsoft.EntityFrameworkCore;
using ResgistroJugadores.Models;
using ResgistroJugadores.Services;

namespace ResgistroJugadores.Context;

public class Contexto : DbContext
{
    public DbSet<Jugadores> Jugadores { get; set; }
    public DbSet<Partida> Partidas { get; set; }
    public DbSet<Movimientos> Movimientos { get; set; }
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // clave foranea para Jugador1
        modelBuilder.Entity<Partida>()
            .HasOne(p => p.Jugador1)
            .WithMany()
            .HasForeignKey(p => p.Jugador1Id)
            .OnDelete(DeleteBehavior.NoAction);

        // clave foranea para Jugador2
        modelBuilder.Entity<Partida>()
            .HasOne(p => p.Jugador2)
            .WithMany()
            .HasForeignKey(p => p.Jugador2Id)
            .OnDelete(DeleteBehavior.NoAction);

        // clave foranea para Ganador
        modelBuilder.Entity<Partida>()
            .HasOne(p => p.Ganador)
            .WithMany()
            .HasForeignKey(p => p.GanadorId)
            .OnDelete(DeleteBehavior.NoAction);

        // clave foranea para turno de jugador
        modelBuilder.Entity<Partida>()
            .HasOne(p => p.TurnoJugador)
            .WithMany()
            .HasForeignKey(p => p.TurnoJugadorId)
            .OnDelete(DeleteBehavior.NoAction);
 

        modelBuilder.Entity<Movimientos>(static entity =>
        {
            entity.HasOne(m => m.Jugadores)
                  .WithMany(static j => j.Movimientos)
                  .HasForeignKey(m => m.JugadorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(m => m.Partidas)
                  .WithMany()
                  .HasForeignKey(m => m.PartidaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

