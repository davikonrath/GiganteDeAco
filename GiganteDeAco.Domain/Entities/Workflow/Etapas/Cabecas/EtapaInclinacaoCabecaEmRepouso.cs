using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;

public class EtapaInclinacaoCabecaEmRepouso(Cabeca cabeca) : EtapaInclinacaoCabeca(cabeca)
{
    public override InclinacaoCabeca Inclinacao => InclinacaoCabeca.EmRepouso;

    public override void Avancar(Response response) => Cabeca.EtapaInclinacao = Cabeca.CriarEtapaInclinacao(InclinacaoCabeca.ParaBaixo);
    public override void Voltar(Response response) => Cabeca.EtapaInclinacao = Cabeca.CriarEtapaInclinacao(InclinacaoCabeca.ParaCima);
}