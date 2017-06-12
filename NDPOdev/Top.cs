using System;
using System.Drawing;

namespace NDPOdev
{
    class Top : Vektor, ICizilebilen
    {

        private int yariCap;
        private Vektor konum;
        private Vektor yon;
        public Top(int x, int y, int yariCap)
        {
            konum = new Vektor(x, y);
            this.yariCap = yariCap;
            yon = new Vektor(4.0f, 4.0f);
        }
        public void hareketEttir()
        {
            konum = konum + yon;
        }
        public bool carpismaVarMi(Vektor p1, Vektor p2)
        {
            Vektor v1 = p2 - p1;
            Vektor merkez = new Vektor(konum.X + yariCap, konum.Y + yariCap);

            Vektor v2 = merkez - p1;

            double Alpha = Math.Acos(Math2D.noktaUret(v1, v2));
            double v2Length = v2.length();
            double mesafe = v2Length * Math.Sin(Alpha);

            if (mesafe <= yariCap) return true;

            return false;

        }
        public void yansima(string yansimaYonu)
        {
            if (yansimaYonu.Equals("UST"))
            {
                this.yon.Y = -yon.Y;
            }
            if (yansimaYonu.Equals("YAN"))
            {
                this.yon.X = -yon.X;
            }

        }
        public void ciz(Graphics grp)
        {
            grp.FillEllipse(Brushes.DarkRed, (int)konum.X, (int)konum.Y, yariCap * 2, yariCap * 2);
        }
        public Vektor Konum
        {
            get
            {
                return this.konum;
            }
            set
            {
                this.konum = value;
            }
        }
        public int YariCap
        {
            get
            {
                return this.yariCap;
            }
            set
            {
                this.yariCap = value;
            }
        }
        public Vektor Yon
        {
            get
            {
                return this.yon;
            }
            set
            {
                this.yon = value;
            }
        }

    }
}
