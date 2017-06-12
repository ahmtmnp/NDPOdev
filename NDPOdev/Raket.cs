using System.Drawing;

namespace NDPOdev
{
    class Raket : ICizilebilen
    {
        private int x;
        private int y;
        private int en = 200;
        private int boy = 20;
        public Raket(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void ciz(Graphics grp)
        {

            grp.FillRectangle(Brushes.Goldenrod, x, y, en, boy);
        }
        public bool carpismaVarMi(Top top, int height)
        {

            if (height - top.Konum.Y <= top.YariCap * 2 + this.boy)
            {
                if (this.x - top.YariCap * 2+5 < top.Konum.X && this.x + 205 > top.Konum.X)
                {
                    return true;
                }

            }

            return false;

        }

        public int X
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
        public int Y
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
