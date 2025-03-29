using GiganteDeAco.Contracts.Common;

namespace GiganteDeAco.Domain.Entities.Workflow
{
    public abstract class Etapa
    {
        protected static void AcaoNaoPermitida(Response response) => response.AddNotificacao(new NotificacaoAcaoNaoPermitida());

        public virtual void Avancar(Response response) => AcaoNaoPermitida(response);
        public virtual void Voltar(Response response) => AcaoNaoPermitida(response);
    }
}