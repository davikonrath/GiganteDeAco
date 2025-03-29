using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;

public abstract class EtapaRotacaoCabeca(Cabeca cabeca) : Etapa
{
    public Cabeca Cabeca { get; set; } = cabeca;

    public virtual RotacaoCabeca Rotacao { get; set; }
}