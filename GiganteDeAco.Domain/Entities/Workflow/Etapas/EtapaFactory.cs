using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Entities.Workflow.Etapas.Cabecas;
using GiganteDeAco.Domain.Entities.Workflow.Etapas.Cotovelos;
using GiganteDeAco.Domain.Entities.Workflow.Etapas.Pulsos;
using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Workflow.Etapas
{
    public static class EtapaFactory
    {
        public static EtapaInclinacaoCabeca CriarEtapaInclinacao(this Cabeca cabeca, InclinacaoCabeca tipoEtapa)
        {
            return tipoEtapa switch
            {
                InclinacaoCabeca.ParaCima => new EtapaInclinacaoCabecaParaCima(cabeca),
                InclinacaoCabeca.EmRepouso => new EtapaInclinacaoCabecaEmRepouso(cabeca),
                InclinacaoCabeca.ParaBaixo => new EtapaInclinacaoCabecaParaBaixo(cabeca),
                _ => throw new ArgumentException("Tipo de etapa desconhecido", nameof(tipoEtapa)),
            };
        }

        public static EtapaRotacaoCabeca CriarEtapaRotacao(this Cabeca cabeca, RotacaoCabeca tipoEtapa)
        {
            return tipoEtapa switch
            {
                RotacaoCabeca.MenosNoventa => new EtapaRotacaoCabecaMenosNoventa(cabeca),
                RotacaoCabeca.MenosQuarentaCinco => new EtapaRotacaoCabecaMenosQuarentaCinco(cabeca),
                RotacaoCabeca.EmRepouso => new EtapaRotacaoCabecaEmRepouso(cabeca),
                RotacaoCabeca.QuarentaCinco => new EtapaRotacaoCabecaQuarentaCinco(cabeca),
                RotacaoCabeca.Noventa => new EtapaRotacaoCabecaNoventa(cabeca),
                _ => throw new ArgumentException("Tipo de etapa desconhecido", nameof(tipoEtapa)),
            };
        }

        public static EtapaRotacaoPulso CriarEtapaRotacao(this Pulso pulso, RotacaoPulso tipoEtapa)
        {
            return tipoEtapa switch
            {
                RotacaoPulso.MenosNoventa => new EtapaRotacaoPulsoMenosNoventa(pulso),
                RotacaoPulso.MenosQuarentaCinco => new EtapaRotacaoPulsoMenosQuarentaCinco(pulso),
                RotacaoPulso.EmRepouso => new EtapaRotacaoPulsoEmRepouso(pulso),
                RotacaoPulso.QuarentaCinco => new EtapaRotacaoPulsoQuarentaCinco(pulso),
                RotacaoPulso.Noventa => new EtapaRotacaoPulsoNoventa(pulso),
                RotacaoPulso.CentoTrintaCinco => new EtapaRotacaoPulsoCentoTrintaCinco(pulso),
                RotacaoPulso.CentoOitenta => new EtapaRotacaoPulsoCentoOitenta(pulso),
                _ => throw new ArgumentException("Tipo de etapa desconhecido", nameof(tipoEtapa)),
            };
        }

        public static EtapaContracaoCotovelo CriarEtapaContracao(this Cotovelo cotovelo, ContracaoCotovelo tipoEtapa)
        {
            return tipoEtapa switch
            {
                ContracaoCotovelo.EmRepouso => new EtapaContracaoCotoveloEmRepouso(cotovelo),
                ContracaoCotovelo.Leve => new EtapaContracaoCotoveloLeve(cotovelo),
                ContracaoCotovelo.Normal => new EtapaContracaoCotoveloNormal(cotovelo),
                ContracaoCotovelo.Forte => new EtapaContracaoCotoveloForte(cotovelo),
                _ => throw new ArgumentException("Tipo de etapa desconhecido", nameof(tipoEtapa)),
            };
        }
    }
}