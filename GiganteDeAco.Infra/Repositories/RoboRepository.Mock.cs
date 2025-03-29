using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Repositories;

namespace GiganteDeAco.Infra.Repositories;

public class RoboRepository : IRoboRepository
{
    private readonly Robo _robo;

    public RoboRepository(Robo robo) => _robo = robo;

    public Task<Robo> ObterRobo() => Task.FromResult(_robo);
}
