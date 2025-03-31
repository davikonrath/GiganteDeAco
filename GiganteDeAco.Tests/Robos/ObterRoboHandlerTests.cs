using GiganteDeAco.Application.Handlers;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;
using GiganteDeAco.Domain.Repositories;
using Moq;
using Xunit;

namespace GiganteDeAco.Tests.Robos;

public class ObterRoboHandlerTests
{
    private readonly Mock<IRoboRepository> _mockRoboRepository;
    private readonly RoboHandler _roboHandler;

    public ObterRoboHandlerTests()
    {
        _mockRoboRepository = new Mock<IRoboRepository>();
        _roboHandler = new RoboHandler(_mockRoboRepository.Object);
    }

    [Fact]
    public async Task HandleAsync_ObterRobo_SucessoRoboNovo()
    {
        var request = new ObterRoboRequest();
        var robo = new Robo();
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _roboHandler.HandleAsync(request);

        Assert.True(response.IsValid());
        Assert.NotNull(response.Robo);
        Assert.Equal((byte)RotacaoCabeca.EmRepouso, response.Robo.Cabeca.Rotacao);
        Assert.Equal((byte)InclinacaoCabeca.EmRepouso, response.Robo.Cabeca.Inclinacao);
        Assert.Equal((byte)ContracaoCotovelo.EmRepouso, response.Robo.BracoEsquerdo.ContracaoCotovelo);
        Assert.Equal((byte)ContracaoCotovelo.EmRepouso, response.Robo.BracoDireito.ContracaoCotovelo);
        Assert.Equal((byte)RotacaoPulso.EmRepouso, response.Robo.BracoEsquerdo.RotacaoPulso);
        Assert.Equal((byte)RotacaoPulso.EmRepouso, response.Robo.BracoDireito.RotacaoPulso);
    }

    [Fact]
    public async Task HandleAsync_ObterRobo_SucessoRoboAlterado()
    {
        var request = new ObterRoboRequest();
        var res = new ObterRoboResponse();
        var robo = new Robo();
        robo.Cabeca.EtapaRotacao.Voltar(res);
        robo.Cabeca.EtapaInclinacao.Voltar(res);
        robo.BracoEsquerdo.Cotovelo.EtapaContracao.Avancar(res);
        robo.BracoEsquerdo.Cotovelo.EtapaContracao.Avancar(res);
        robo.BracoEsquerdo.Cotovelo.EtapaContracao.Avancar(res);
        robo.BracoEsquerdo.Pulso.EtapaRotacao.Avancar(res);
        robo.BracoDireito.Cotovelo.EtapaContracao.Avancar(res);
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _roboHandler.HandleAsync(request);

        Assert.True(response.IsValid());
        Assert.NotNull(response.Robo);
        Assert.Equal((byte)RotacaoCabeca.MenosQuarentaCinco, response.Robo.Cabeca.Rotacao);
        Assert.Equal((byte)InclinacaoCabeca.ParaCima, response.Robo.Cabeca.Inclinacao);
        Assert.Equal((byte)ContracaoCotovelo.Forte, response.Robo.BracoEsquerdo.ContracaoCotovelo);
        Assert.Equal((byte)ContracaoCotovelo.Leve, response.Robo.BracoDireito.ContracaoCotovelo);
        Assert.Equal((byte)RotacaoPulso.QuarentaCinco, response.Robo.BracoEsquerdo.RotacaoPulso);
        Assert.Equal((byte)RotacaoPulso.EmRepouso, response.Robo.BracoDireito.RotacaoPulso);
    }

    [Fact]
    public async Task HandleAsync_ObterRobo_RoboNaoEncontrado()
    {
        var request = new ObterRoboRequest();
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync((Robo?)null);

        var response = await _roboHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Robo não encontrado.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 404);
    }
}