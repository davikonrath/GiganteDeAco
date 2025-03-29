namespace GiganteDeAco.Contracts.Common;

public class NotificacaoBadRequest : Notificacao
{
    public const int CodigoNaoEncontrado = 400;

    public NotificacaoBadRequest(string mensagem) : base(CodigoNaoEncontrado)
    {
        Mensagem = mensagem;
    }
}