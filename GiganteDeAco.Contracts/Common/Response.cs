using System.Text.Json.Serialization;

namespace GiganteDeAco.Contracts.Common;

public abstract class Response
{
    [JsonInclude]
    public ICollection<Notificacao>? Notificacoes;

    public void AddNotificacao(Notificacao notificacao)
    {
        if (Notificacoes == null)
            Notificacoes = [notificacao];
        else
            Notificacoes.Add(notificacao);
    }

    public bool IsValid() => Notificacoes == null || Notificacoes.Count == 0;
}