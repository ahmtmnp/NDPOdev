namespace NDPOdev
{
    class Math2D
    {
        public static double noktaUret(Vektor v1, Vektor v2)
        {
            Vektor v3 = v1.normalize();
            Vektor v4 = v2.normalize();

            return v3.X * v4.X + v3.Y * v4.Y;
        }
    }
}
