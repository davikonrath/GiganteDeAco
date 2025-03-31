using GiganteDeAco.Application.Handlers;
using GiganteDeAco.Contracts.Commands.Cabecas.Inclinacao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;
using GiganteDeAco.Domain.Repositories;
using Moq;
using Xunit;

namespace GiganteDeAco.Tests.Cabecas;

public class VoltarInclinacaoCabecaHandlerTests
{
    private readonly Mock<IRoboRepository> _mockRoboRepository;
    private readonly CabecaHandler _cabecaHandler;

    public VoltarInclinacaoCabecaHandlerTests()
    {
        _mockRoboRepository = new Mock<IRoboRepository>();
        _cabecaHandler = new CabecaHandler(_mockRoboRepository.Object);
    }

    [Fact]
    public async Task HandleAsync_VoltarInclinacaoCabeca_Sucesso()
    {
        var request = new VoltarInclinacaoCabecaRequest();
        var robo = new Robo();
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _cabecaHandler.HandleAsync(request);

        Assert.True(response.IsValid());
        Assert.NotNull(response.Robo);
        Assert.Equal((byte)InclinacaoCabeca.ParaCima, response.Robo.Cabeca.Inclinacao);
    }

    [Fact]
    public async Task HandleAsync_VoltarInclinacaoCabeca_RoboNaoEncontrado()
    {
        var request = new VoltarInclinacaoCabecaRequest();
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync((Robo?)null);

        var response = await _cabecaHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Robo não encontrado.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 404);
    }

    [Fact]
    public async Task HandleAsync_VoltarInclinacaoCabeca_AcaoNaoPermitidaLimiteMin()
    {
        var request = new VoltarInclinacaoCabecaRequest();
        var robo = new Robo();
        robo.Cabeca.EtapaInclinacao.Voltar(new ObterRoboResponse());
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _cabecaHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Ação não permitida no status atual.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 403);
    }
}