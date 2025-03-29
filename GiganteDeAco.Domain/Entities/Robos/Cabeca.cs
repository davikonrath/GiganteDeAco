using GiganteDeAco.Domain.Entities.Workflow.Etapas;
using GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Robos;

public class Cabeca
{
    public Cabeca()
    {
        EtapaInclinacao = EtapaFactory.CriarEtapaInclinacao(this, InclinacaoCabeca.EmRepouso);
        EtapaRotacao = EtapaFactory.CriarEtapaRotacao(this, RotacaoCabeca.EmRepouso);
    }

    public EtapaInclinacaoCabeca EtapaInclinacao { get; set; }
    public EtapaRotacaoCabeca EtapaRotacao { get; set; }
    public InclinacaoCabeca Inclinacao => EtapaInclinacao.Inclinacao;
    public RotacaoCabeca Rotacao => EtapaRotacao.Rotacao;
}
