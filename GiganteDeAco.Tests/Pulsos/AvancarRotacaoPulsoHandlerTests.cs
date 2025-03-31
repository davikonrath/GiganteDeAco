using GiganteDeAco.Application.Handlers;
using GiganteDeAco.Contracts.Commands.Pulsos.Rotacao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;
using GiganteDeAco.Domain.Repositories;
using Moq;
using Xunit;

namespace GiganteDeAco.Tests.Pulsos;

public class AvancarRotacaoPulsoHandlerTests
{
    private readonly Mock<IRoboRepository> _mockRoboRepository;
    private readonly PulsoHandler _pulsoHandler;

    public AvancarRotacaoPulsoHandlerTests()
    {
        _mockRoboRepository = new Mock<IRoboRepository>();
        _pulsoHandler = new PulsoHandler(_mockRoboRepository.Object);
    }

    [Theory]
    [InlineData((byte)Lado.Esquerdo)]
    [InlineData((byte)Lado.Direito)]
    public async Task HandleAsync_AvancarRotacaoPulso_Sucesso(byte lado)
    {
        var request = new AvancarRotacaoPulsoRequest() { Lado = lado };
        var res = new ObterRoboResponse();
        var robo = new Robo();
        var bracoRobo = lado == (byte)Lado.Esquerdo ? robo.BracoEsquerdo : robo.BracoDireito;
        for (int i = 0; i < 3; i++)
            bracoRobo.Cotovelo.EtapaContracao.Avancar(res);

        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _pulsoHandler.HandleAsync(request);

        Assert.True(response.IsValid());
        Assert.NotNull(response.Robo);

        var braco = lado == (byte)Lado.Esquerdo ? response.Robo.BracoEsquerdo : response.Robo.BracoDireito;
        Assert.Equal((byte)RotacaoPulso.QuarentaCinco, braco.RotacaoPulso);
    }

    [Fact]
    public async Task HandleAsync_AvancarRotacaoPulso_RoboNaoEncontrado()
    {
        var request = new AvancarRotacaoPulsoRequest();
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync((Robo?)null);

        var response = await _pulsoHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Robo não encontrado.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 404);
    }

    [Fact]
    public async Task HandleAsync_AvancarRotacaoPulso_AcaoNaoPermitida()
    {
        var request = new AvancarRotacaoPulsoRequest();
        var robo = new Robo();
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _pulsoHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Ação não permitida no status atual.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 403);
    }    
    
    [Fact]
    public async Task HandleAsync_AvancarRotacaoPulso_AcaoNaoPermitidaLimiteMax()
    {
        var request = new AvancarRotacaoPulsoRequest() { Lado = (byte)Lado.Esquerdo };
        var res = new ObterRoboResponse();
        var robo = new Robo();
        for (int i = 0; i < 3; i++)
            robo.BracoEsquerdo.Cotovelo.EtapaContracao.Avancar(res);  
        
        for (int i = 0; i < 4; i++)
            robo.BracoEsquerdo.Pulso.EtapaRotacao.Avancar(res);
        
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _pulsoHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Ação não permitida no status atual.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 403);
    }
}