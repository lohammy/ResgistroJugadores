using Microsoft.EntityFrameworkCore;
using ResgistroJugadores.Models;

namespace ResgistroJugadores.Context;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Jugadores> Jugadores { get; set; }
}
