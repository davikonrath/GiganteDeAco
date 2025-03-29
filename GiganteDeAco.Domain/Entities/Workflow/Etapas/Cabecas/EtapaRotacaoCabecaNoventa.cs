using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;

public class EtapaRotacaoCabecaNoventa(Cabeca cabeca) : EtapaRotacaoCabeca(cabeca)
{
    public override RotacaoCabeca Rotacao => RotacaoCabeca.Noventa;

    public override void Voltar(Response response) => Cabeca.EtapaRotacao = Cabeca.CriarEtapaRotacao(RotacaoCabeca.QuarentaCinco);
}