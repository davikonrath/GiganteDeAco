namespace GiganteDeAco.Contracts.Common;

public class NotificacaoNaoEncontrado : Notificacao
{
    public const int CodigoNaoEncontrado = 404;

    public NotificacaoNaoEncontrado(string registro) : base(CodigoNaoEncontrado)
    {
        Mensagem = $"{registro} não encontrado.";
    }
}