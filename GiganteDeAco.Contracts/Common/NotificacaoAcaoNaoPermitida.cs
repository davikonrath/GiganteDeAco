namespace GiganteDeAco.Contracts.Common;

public class NotificacaoAcaoNaoPermitida : Notificacao
{
    public const int CodigoNaoPermitido = 403;

    public NotificacaoAcaoNaoPermitida() : base(CodigoNaoPermitido)
    {
        Mensagem = $"A��o n�o permitida no status atual.";
    }
}