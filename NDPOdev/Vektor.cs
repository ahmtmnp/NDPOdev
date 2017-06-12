using System;

namespace NDPOdev
{
    class Vektor
    {
        private double x;
        private double y;
        public Vektor()
        {

        }
        public Vektor(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Vektor normalize()
        {
            double length = Math.Sqrt((x * x + y * y));
            return new Vektor(x / length, y / length);
        }
        public double length()
        {
            return Math.Sqrt(x * x + y * y);
        }
        public static Vektor operator +(Vektor v1, Vektor v2)
        {
            return new Vektor(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vektor operator -(Vektor v1, Vektor v2)
        {
            return new Vektor(v1.x - v2.x, v1.y - v2.y);
        }
        public static Vektor operator *(Vektor v1, double sayi)
        {
            return new Vektor(v1.x * sayi, v1.y * sayi);
        }
        public static Vektor operator /(Vektor v1, double sayi)
        {
            return new Vektor(v1.x / sayi, v1.y / sayi);
        }
        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
    }
}
