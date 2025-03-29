using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;

public class EtapaInclinacaoCabecaParaBaixo(Cabeca cabeca) : EtapaInclinacaoCabeca(cabeca)
{
    public override InclinacaoCabeca Inclinacao => InclinacaoCabeca.ParaBaixo;

    public override void Voltar(Response response) => Cabeca.EtapaInclinacao = Cabeca.CriarEtapaInclinacao(InclinacaoCabeca.EmRepouso);
}