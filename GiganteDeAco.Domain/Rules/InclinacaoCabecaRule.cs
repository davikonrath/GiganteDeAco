using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;

namespace GiganteDeAco.Domain.Rules;

public static class InclinacaoCabecaRule
{
    public static void ValidarAvancar(Robo robo, Response response)
    {
        if (robo.Cabeca.EtapaRotacao is not EtapaRotacaoCabecaEmRepouso &&
            robo.Cabeca.EtapaInclinacao is EtapaInclinacaoCabecaEmRepouso)
            response.AddNotificacao(new NotificacaoAcaoNaoPermitida());
    }
}
