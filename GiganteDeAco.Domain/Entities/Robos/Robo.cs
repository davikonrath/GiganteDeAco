using GiganteDeAco.Domain.Enums;

namespace GiganteDeAco.Domain.Entities.Robos
{
    public class Robo
    {
        public Robo()
        {
            Cabeca = new Cabeca();
            Bracos = [new(Lado.Esquerdo), new(Lado.Direito)];
        }

        public Cabeca Cabeca { get; set; }
        public IEnumerable<Braco> Bracos { get; set; }
        public Braco BracoEsquerdo => Bracos.First(b => b.Lado == Lado.Esquerdo);
        public Braco BracoDireito => Bracos.First(b => b.Lado == Lado.Direito);
    }
}
