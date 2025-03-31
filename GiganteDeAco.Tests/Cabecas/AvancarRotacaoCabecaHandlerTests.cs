using GiganteDeAco.Application.Handlers;
using GiganteDeAco.Contracts.Commands.Cabecas.Rotacao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;
using GiganteDeAco.Domain.Repositories;
using Moq;
using Xunit;

namespace GiganteDeAco.Tests.Cabecas;

public class AvancarRotacaoCabecaHandlerTests
{
    private readonly Mock<IRoboRepository> _mockRoboRepository;
    private readonly CabecaHandler _cabecaHandler;

    public AvancarRotacaoCabecaHandlerTests()
    {
        _mockRoboRepository = new Mock<IRoboRepository>();
        _cabecaHandler = new CabecaHandler(_mockRoboRepository.Object);
    }

    [Fact]
    public async Task HandleAsync_AvancarRotacaoCabeca_Sucesso()
    {
        var request = new AvancarRotacaoCabecaRequest();
        var robo = new Robo();
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _cabecaHandler.HandleAsync(request);

        Assert.True(response.IsValid());
        Assert.NotNull(response.Robo);
        Assert.Equal((byte)RotacaoCabeca.QuarentaCinco, response.Robo.Cabeca.Rotacao);
    }

    [Fact]
    public async Task HandleAsync_AvancarRotacaoCabeca_RoboNaoEncontrado()
    {
        var request = new AvancarRotacaoCabecaRequest();
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync((Robo?)null);

        var response = await _cabecaHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Robo não encontrado.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 404);
    }

    [Fact]
    public async Task HandleAsync_AvancarRotacaoCabeca_AcaoNaoPermitida()
    {
        var request = new AvancarRotacaoCabecaRequest();
        var res = new ObterRoboResponse();
        var robo = new Robo();
        robo.Cabeca.EtapaInclinacao.Avancar(res);
        robo.Cabeca.EtapaRotacao.Avancar(res);
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _cabecaHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Ação não permitida no status atual.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 403);
    }

    [Fact]
    public async Task HandleAsync_AvancarRotacaoCabeca_AcaoNaoPermitidaLimiteMax()
    {
        var request = new AvancarRotacaoCabecaRequest();
        var res = new ObterRoboResponse();
        var robo = new Robo();
        for (int i = 0; i < 2; i++)
            robo.Cabeca.EtapaRotacao.Avancar(res);

        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _cabecaHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Ação não permitida no status atual.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 403);
    }
}