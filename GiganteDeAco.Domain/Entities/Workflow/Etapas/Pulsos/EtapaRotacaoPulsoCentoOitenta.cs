using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Pulsos;

public class EtapaRotacaoPulsoCentoOitenta(Pulso pulso) : EtapaRotacaoPulso(pulso)
{
    public override RotacaoPulso Rotacao => RotacaoPulso.CentoOitenta;

    public override void Voltar(Response response) => Pulso.EtapaRotacao = Pulso.CriarEtapaRotacao(RotacaoPulso.CentoTrintaCinco);
}