namespace GiganteDeAco.Contracts.Common;

public class NotificacaoAcaoNaoPermitida : Notificacao
{
    public const int CodigoNaoPermitido = 403;

    public NotificacaoAcaoNaoPermitida() : base(CodigoNaoPermitido)
    {
        Mensagem = $"Ação não permitida no status atual.";
    }
}