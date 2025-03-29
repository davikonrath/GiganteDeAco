using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cotovelos;

public class EtapaContracaoCotoveloForte(Cotovelo cotovelo) : EtapaContracaoCotovelo(cotovelo)
{
    public override ContracaoCotovelo Contracao => ContracaoCotovelo.Forte;

    public override void Voltar(Response response) => Cotovelo.EtapaContracao = Cotovelo.CriarEtapaContracao(ContracaoCotovelo.Normal);
}