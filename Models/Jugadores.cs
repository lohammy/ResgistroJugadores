using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResgistroJugadores.Models;

public class Jugadores

{
    [Key]
    public int JugadorId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]

    [Range(1, int.MaxValue, ErrorMessage = "las partidas jugadas no pueden ser menor a 1")]

    public int Victorias { get; set; }

    public int Empates { get; set; }

    public int Derrotas { get; set; }

    [InverseProperty(nameof(Models.Movimientos.Jugadores))]
    public virtual ICollection<Movimientos> Movimientos { get; set; }

}
