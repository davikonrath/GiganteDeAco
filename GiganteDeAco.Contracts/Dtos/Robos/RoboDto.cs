namespace GiganteDeAco.Contracts.Dtos.Robos;

public class RoboDto
{
    public required CabecaDto Cabeca { get; set; }
    public required BracoDto BracoEsquerdo { get; set; }
    public required BracoDto BracoDireito { get; set; }
}