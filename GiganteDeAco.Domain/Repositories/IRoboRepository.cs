using GiganteDeAco.Domain.Entities.Robos;

namespace GiganteDeAco.Domain.Repositories;

public interface IRoboRepository
{
    Task<Robo> ObterRobo();
}
