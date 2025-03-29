using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;

public abstract class EtapaInclinacaoCabeca(Cabeca cabeca) : Etapa
{
    public Cabeca Cabeca { get; set; } = cabeca;

    public virtual InclinacaoCabeca Inclinacao { get; set; }
}