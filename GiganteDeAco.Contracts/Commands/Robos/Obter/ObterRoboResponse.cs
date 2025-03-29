using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Contracts.Dtos.Robos;

namespace GiganteDeAco.Contracts.Commands.Robos.Obter;

public class ObterRoboResponse : Response
{
    public ObterRoboResponse()
    { }

    public ObterRoboResponse(RoboDto robo) => Robo = robo;

    public RoboDto? Robo { get; set; }
}
