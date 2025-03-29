using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cotovelos;

public class EtapaContracaoCotoveloEmRepouso(Cotovelo cotovelo) : EtapaContracaoCotovelo(cotovelo)
{
    public override ContracaoCotovelo Contracao => ContracaoCotovelo.EmRepouso;

    public override void Avancar(Response response) => Cotovelo.EtapaContracao = Cotovelo.CriarEtapaContracao(ContracaoCotovelo.Leve);
}