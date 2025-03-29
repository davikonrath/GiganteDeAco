using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cotovelos;

public abstract class EtapaContracaoCotovelo(Cotovelo cotovelo) : Etapa
{
    public Cotovelo Cotovelo { get; set; } = cotovelo;

    public virtual ContracaoCotovelo Contracao { get; set; }
}