using GiganteDeAco.Domain.Entities.Workflow.Etapas.Pulsos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Robos;

public class Pulso
{
    public Pulso() => EtapaRotacao = new EtapaRotacaoPulsoEmRepouso(this);

    public EtapaRotacaoPulso EtapaRotacao { get; set; }
    public RotacaoPulso Rotacao => EtapaRotacao.Rotacao;
}
