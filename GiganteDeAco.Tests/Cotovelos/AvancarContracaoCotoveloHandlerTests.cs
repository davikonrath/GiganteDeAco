﻿using GiganteDeAco.Application.Handlers;
using GiganteDeAco.Contracts.Commands.Cotovelos.Contracao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;
using GiganteDeAco.Domain.Repositories;
using Moq;
using Xunit;

namespace GiganteDeAco.Tests.Cotovelos;

public class AvancarContracaoCotoveloHandlerTests
{
    private readonly Mock<IRoboRepository> _mockRoboRepository;
    private readonly CotoveloHandler _cotoveloHandler;

    public AvancarContracaoCotoveloHandlerTests()
    {
        _mockRoboRepository = new Mock<IRoboRepository>();
        _cotoveloHandler = new CotoveloHandler(_mockRoboRepository.Object);
    }

    [Theory]
    [InlineData((byte)Lado.Esquerdo)]
    [InlineData((byte)Lado.Direito)]
    public async Task HandleAsync_AvancarContracaoCotovelo_Sucesso(byte lado)
    {
        var request = new AvancarContracaoCotoveloRequest() { Lado = lado };
        var robo = new Robo();

        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _cotoveloHandler.HandleAsync(request);

        Assert.True(response.IsValid());
        Assert.NotNull(response.Robo);

        var braco = lado == (byte)Lado.Esquerdo ? response.Robo.BracoEsquerdo : response.Robo.BracoDireito;
        Assert.Equal((byte)ContracaoCotovelo.Leve, braco.ContracaoCotovelo);
    }

    [Fact]
    public async Task HandleAsync_AvancarContracaoCotovelo_RoboNaoEncontrado()
    {
        var request = new AvancarContracaoCotoveloRequest() { Lado = (byte)Lado.Esquerdo };
        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync((Robo?)null);

        var response = await _cotoveloHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Robo não encontrado.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 404);
    }

    [Fact]
    public async Task HandleAsync_AvancarContracaoCotovelo_AcaoNaoPermitidaLimiteMax()
    {
        var request = new AvancarContracaoCotoveloRequest() { Lado = (byte)Lado.Esquerdo };
        var res = new ObterRoboResponse();
        var robo = new Robo();
        for (int i = 0; i < 3; i++)
            robo.BracoEsquerdo.Cotovelo.EtapaContracao.Avancar(res);

        _mockRoboRepository.Setup(repo => repo.ObterRobo()).ReturnsAsync(robo);

        var response = await _cotoveloHandler.HandleAsync(request);

        Assert.False(response.IsValid());
        Assert.NotNull(response.Notificacoes);
        Assert.Contains(response.Notificacoes, n => n.Mensagem == "Ação não permitida no status atual.");
        Assert.Contains(response.Notificacoes, n => n.Codigo == 403);
    }
}