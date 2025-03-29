using GiganteDeAco.Domain.Entities.Workflow.Etapas;
using GiganteDeAco.Domain.Entities.Workflow.Etapas.Cotovelos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Robos;

public class Cotovelo
{
    public Cotovelo() => EtapaContracao = EtapaFactory.CriarEtapaContracao(this, ContracaoCotovelo.EmRepouso);

    public EtapaContracaoCotovelo EtapaContracao { get; set; }
    public ContracaoCotovelo Contracao => EtapaContracao.Contracao;
}
