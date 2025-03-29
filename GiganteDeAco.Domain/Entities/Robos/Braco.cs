using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Robos;

public class Braco
{
    public Braco(Lado lado)
    {
        Cotovelo = new Cotovelo();
        Pulso = new Pulso();
        Lado = lado;
    }

    public Cotovelo Cotovelo { get; set; }
    public Pulso Pulso { get; set; }
    public Lado Lado { get; set; }
}
