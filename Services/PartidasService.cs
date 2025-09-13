using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ResgistroJugadores.Context;
using ResgistroJugadores.Models;

namespace ResgistroJugadores.Services;

public class PartidasService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Registrar(Partida partidas)
    {


        if (partidas.Jugador1Id != 0 && partidas.Jugador1Id != partidas.Jugador2Id)
        {
            return await Insertar(partidas);
        }
        return false;
    }
    private async Task<bool> Existe(int partidaId)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Partidas
            .AnyAsync(t => t.PartidaId == partidaId);
    }

    private async Task<bool> Insertar(Partida partida)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Partidas.Add(partida);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Partida partida)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(partida );
        return await contexto
            .SaveChangesAsync() > 0;
    }
    public async Task<Partida?> Buscar(int partidaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas
            .FirstOrDefaultAsync(p => p.PartidaId == partidaId);
    }
    public async Task<bool> Eliminar(int partidaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas
            .AsNoTracking()
            .Where(p => p.PartidaId == partidaId)
            .ExecuteDeleteAsync() > 0;
    }
    public async Task<List<Partida>> Listar(Expression<Func<Partida, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas
            .Include(p => p.Jugador1)
            .Include(p => p.Jugador2)
            .Where(criterio)
            .ToListAsync();

    }

}
