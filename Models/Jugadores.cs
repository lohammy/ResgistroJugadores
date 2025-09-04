using System.ComponentModel.DataAnnotations;

namespace ResgistroJugadores.Models;

public class Jugadores

{
    [Key]
    public int JugadorId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]

    [Range(1, int.MaxValue, ErrorMessage = "las partidas jugadas no pueden ser menor a 1")]

    public int Partida { get; set; }


}
