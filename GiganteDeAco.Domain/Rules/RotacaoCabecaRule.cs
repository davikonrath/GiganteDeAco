using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;

namespace GiganteDeAco.Domain.Rules;

public static class RotacaoCabecaRule
{
    public static void ValidarAvancar(Robo robo, Response response)
    {
        if (robo.Cabeca.EtapaInclinacao is EtapaInclinacaoCabecaParaBaixo)
            response.AddNotificacao(new NotificacaoAcaoNaoPermitida());
    }

    public static void ValidarVoltar(Robo robo, Response response)
    {
        if (robo.Cabeca.EtapaInclinacao is EtapaInclinacaoCabecaParaBaixo)
            response.AddNotificacao(new NotificacaoAcaoNaoPermitida());
    }
}
