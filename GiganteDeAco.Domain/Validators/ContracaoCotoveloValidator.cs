﻿using GiganteDeAco.Contracts.Commands.Cotovelos.Contracao;
using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Validators;

public static class ContracaoCotoveloValidator
{
    public static void Validar(this AvancarContracaoCotoveloRequest request, Response response)
    {
        if (request is null)
        {
            response.AddNotificacao(new NotificacaoBadRequest("Request não deve ser nulo."));
            return;
        }

        if (!Enum.IsDefined(typeof(Lado), request.Lado))
            response.AddNotificacao(new NotificacaoBadRequest("Valor inválido para o enum Lado."));
    }

    public static void Validar(this VoltarContracaoCotoveloRequest request, Response response)
    {
        if (request is null)
        {
            response.AddNotificacao(new NotificacaoBadRequest("Request não deve ser nulo."));
            return;
        }

        if (!Enum.IsDefined(typeof(Lado), request.Lado))
            response.AddNotificacao(new NotificacaoBadRequest("Valor inválido para o enum Lado."));
    }
}
