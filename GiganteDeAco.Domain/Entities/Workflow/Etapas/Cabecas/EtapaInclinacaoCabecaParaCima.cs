﻿using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;

public class EtapaInclinacaoCabecaParaCima(Cabeca cabeca) : EtapaInclinacaoCabeca(cabeca)
{
    public override InclinacaoCabeca Inclinacao => InclinacaoCabeca.ParaCima;

    public override void Avancar(Response response) => Cabeca.EtapaInclinacao = Cabeca.CriarEtapaInclinacao(InclinacaoCabeca.EmRepouso);
}