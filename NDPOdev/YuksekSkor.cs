using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace NDPOdev
{
    public partial class YuksekSkor : Form
    {
        Panel pSkor = new Panel();
        Button bTamam = new Button();
        Label lbAdlar = new Label();
        Label lbSkorlar = new Label();
        String strSureler="",strAdlar="";

        public YuksekSkor(int width, int height,ArrayList adlar,ArrayList sureler)
        {
            foreach (string str in adlar) {
                strAdlar += str + Environment.NewLine;
            }
            foreach (string str in sureler)
            {
                string sure = str.Substring(0, 2) + ":" + str.Substring(2, 2) + ":" + str.Substring(4, 3);
                strSureler += sure + Environment.NewLine;
            }

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Yüksek Skorlar";
            this.SetBounds((Screen.GetBounds(this).Width / 2) - (width / 2), (Screen.GetBounds(this).Height / 2) - (height / 2), width, height);
            pSkor.SetBounds(5, 5, this.ClientSize.Width - 10, this.ClientSize.Height - 40);
            pSkor.BorderStyle = BorderStyle.FixedSingle;
            pSkor.BackColor = Color.DarkBlue;
            Controls.Add(pSkor);
            SetBounds((Screen.GetBounds(this).Width / 2) - (width / 2), (Screen.GetBounds(this).Height / 2) - (height / 2), width, height);
            

            bTamam.SetBounds(this.ClientSize.Width - 85, this.ClientSize.Height - 30, 80, 25);
            bTamam.Text = "Tamam";
            Controls.Add(bTamam);
            
            lbAdlar.BorderStyle = BorderStyle.None;
            lbAdlar.TextAlign = ContentAlignment.TopLeft;
            lbAdlar.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lbAdlar.SetBounds(0, 0, (pSkor.ClientSize.Width) - 90, pSkor.ClientSize.Height);
            lbAdlar.Text = strAdlar; 
            lbAdlar.BackColor = pSkor.BackColor;
            lbAdlar.ForeColor = Color.White;
            pSkor.Controls.Add(lbAdlar);

            lbSkorlar.BorderStyle = BorderStyle.None;
            lbSkorlar.TextAlign = ContentAlignment.TopCenter;
            lbSkorlar.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lbSkorlar.SetBounds((pSkor.ClientSize.Width) - 90, 0, 90, 125);
            lbSkorlar.Text = strSureler;
            lbSkorlar.BackColor = pSkor.BackColor;
            lbSkorlar.ForeColor = Color.White;
            pSkor.Controls.Add(lbSkorlar);

            bTamam.Click += BTamam_Click;

        }

        private void BTamam_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
