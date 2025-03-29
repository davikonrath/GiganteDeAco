using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Pulsos;

public abstract class EtapaRotacaoPulso(Pulso pulso) : Etapa
{
    public Pulso Pulso { get; set; } = pulso;

    public virtual RotacaoPulso Rotacao { get; set; }
}
