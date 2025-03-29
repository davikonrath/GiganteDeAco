using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Pulsos;

public class EtapaRotacaoPulsoNoventa(Pulso pulso) : EtapaRotacaoPulso(pulso)
{
    public override RotacaoPulso Rotacao => RotacaoPulso.Noventa;

    public override void Avancar(Response response) => Pulso.EtapaRotacao = Pulso.CriarEtapaRotacao(RotacaoPulso.CentoTrintaCinco);
    public override void Voltar(Response response) => Pulso.EtapaRotacao = Pulso.CriarEtapaRotacao(RotacaoPulso.QuarentaCinco);
}