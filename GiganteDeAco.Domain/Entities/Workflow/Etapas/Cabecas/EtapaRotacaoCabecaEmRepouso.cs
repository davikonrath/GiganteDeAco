using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;

public class EtapaRotacaoCabecaEmRepouso(Cabeca cabeca) : EtapaRotacaoCabeca(cabeca)
{
    public override RotacaoCabeca Rotacao => RotacaoCabeca.EmRepouso;

    public override void Avancar(Response response) => Cabeca.EtapaRotacao = Cabeca.CriarEtapaRotacao(RotacaoCabeca.QuarentaCinco);
    public override void Voltar(Response response) => Cabeca.EtapaRotacao = Cabeca.CriarEtapaRotacao(RotacaoCabeca.MenosQuarentaCinco);
}