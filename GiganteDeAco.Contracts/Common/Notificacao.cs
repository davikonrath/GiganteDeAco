using System.Text.Json.Serialization;

namespace GiganteDeAco.Contracts.Common;

public class Notificacao
{
    public Notificacao(int codigo, string mensagem)
    {
        Mensagem = mensagem;
        Codigo = codigo;
    }

    public Notificacao(int codigo)
    {
        Codigo = codigo;
    }

    [JsonInclude]
    public int Codigo;
    
    [JsonInclude]
    public string? Mensagem;
}