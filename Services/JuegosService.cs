using ResgistroJugadores.Models;

namespace ResgistroJugadores.Services
{
    public class JuegosServices
    {
        public enum PlayerType { X, O };

        private JugadorService _jugadorService { get; set; } = default!;

        private PartidasService _partidasService { get; set; } = default!;
        private PlayerType?[] board = new PlayerType?[9];

        public JuegosServices(JugadorService jugadorService, PartidasService partidasService)
        {
            _jugadorService = jugadorService;
            ;
            _partidasService = partidasService;

        }

        public PlayerType? CheckForWinner(PlayerType?[] board)
        {

            var winningLines = new[]
            {
             new[] {0, 1, 2}, new[] {3, 4, 5}, new[] {6, 7, 8},// Horizontales
             new[] {0, 3, 6}, new[] {1, 4, 7}, new[] {2, 5, 8},// Verticales
             new[] {0, 4, 8}, new[] {2, 4, 6}// Diagonales
        };

            foreach (var line in winningLines)
            {
                var (a, b, c) = (line[0], line[1], line[2]);
                if (board[a].HasValue && board[a] == board[b] && board[a] == board[c])
                {
                    return board[a];
                }
            }

            return null; // No hay ganador
        }
        public bool esEmpate(PlayerType?[] board)
        {
            // Comprobar empate
            return board.All(cell => cell != null);
        }

        public async Task GameOver(Partida partida, Jugadores jugador1, Jugadores jugador2, PlayerType? ganador)
        {
            if (partida == null)
                throw new ArgumentNullException(nameof(partida));

            if (jugador1 == null || jugador2 == null)
                throw new ArgumentNullException("Los jugadores no pueden ser null");

            partida.FechaFin = DateTime.Now;
            partida.EstadoPartida = "Finalizada";
            //estado de tablero

            if (ganador.HasValue)
            {
                partida.GanadorId = ganador == PlayerType.X ? jugador1?.JugadorId : jugador2?.JugadorId;

                if (ganador == PlayerType.X)
                {
                    jugador1.Victorias++;
                    jugador2.Derrotas++;
                }
                else
                {
                    jugador1.Derrotas++;
                    jugador2.Victorias++;
                }
            }
            else
            {

                jugador1.Empates++;
                jugador2.Empates++;
            }

            await _partidasService.Modificar(partida);

            if (jugador1 != null) await _jugadorService.Modificar(jugador1);
            if (jugador2 != null) await _jugadorService.Modificar(jugador2);
        }
    }
}
