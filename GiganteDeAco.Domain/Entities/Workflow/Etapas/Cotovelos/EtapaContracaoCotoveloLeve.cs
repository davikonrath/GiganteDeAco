using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cotovelos;

public class EtapaContracaoCotoveloLeve(Cotovelo cotovelo) : EtapaContracaoCotovelo(cotovelo)
{
    public override ContracaoCotovelo Contracao => ContracaoCotovelo.Leve;

    public override void Avancar(Response response) => Cotovelo.EtapaContracao = Cotovelo.CriarEtapaContracao(ContracaoCotovelo.Normal);
    public override void Voltar(Response response) => Cotovelo.EtapaContracao = Cotovelo.CriarEtapaContracao(ContracaoCotovelo.EmRepouso);
}