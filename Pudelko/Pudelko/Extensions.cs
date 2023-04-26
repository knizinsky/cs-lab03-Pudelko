namespace Pudelko1
{
    public static class PudelkoExtensions
    {
        public static Pudelko Kompresuj(this Pudelko pudelko)
        {
            double a = Math.Cbrt(pudelko.Objetosc);
            return new Pudelko(a, a, a);
        }
    }
}