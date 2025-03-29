using GiganteDeAco.Contracts.Dtos.Robos;
using GiganteDeAco.Domain.Entities.Robos;

namespace GiganteDeAco.Application.Mappers;

public class RoboMapper
{
    public static RoboDto Map(Robo robo)
    {
        return new RoboDto()
        {
            Cabeca = new CabecaDto()
            {
                Inclinacao = (byte)robo.Cabeca.Inclinacao,
                Rotacao = (byte)robo.Cabeca.Rotacao
            },
            BracoEsquerdo = new BracoDto()
            {
                ContracaoCotovelo = (byte)robo.BracoEsquerdo.Cotovelo.Contracao,
                RotacaoPulso = (byte)robo.BracoEsquerdo.Pulso.Rotacao,
            },
            BracoDireito = new BracoDto()
            {
                ContracaoCotovelo = (byte)robo.BracoDireito.Cotovelo.Contracao,
                RotacaoPulso = (byte)robo.BracoDireito.Pulso.Rotacao,
            },
        };
    }
}