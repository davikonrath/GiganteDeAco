using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Entities.Workflow.Etapas.Pulsos;

namespace GiganteDeAco.Domain.Rules;

public static class ContracaoCotoveloRule
{  
    public static void ValidarVoltar(Braco braco, Response response)
    {
        if (braco.Pulso.EtapaRotacao is not EtapaRotacaoPulsoEmRepouso)
            response.AddNotificacao(new NotificacaoAcaoNaoPermitida());
    }
}