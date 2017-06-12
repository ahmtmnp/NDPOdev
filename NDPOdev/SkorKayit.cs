using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NDPOdev
{
    public partial class SkorKayit : Form
    {
        Panel pSkor = new Panel();
        Button bTamam = new Button();
        Button bVazgec = new Button();
        Label lbtebrik = new Label();
        Label lbAd = new Label();
        Label lbSkorSaat = new Label();
        TextBox txtAd = new TextBox();
        ArrayList skorSureler;
        ArrayList skorAdlar;
        String yuksekSkor;
        public SkorKayit(int width, int height, string skor,ArrayList sureler,ArrayList adlar)
        {
            skorSureler = sureler;
            skorAdlar = adlar;
            yuksekSkor = skor.Replace(@":", string.Empty);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Yüksek Skor";
            this.SetBounds((Screen.GetBounds(this).Width / 2) - (width / 2), (Screen.GetBounds(this).Height / 2) - (height / 2), width, height);
            pSkor.SetBounds(5, 5, this.ClientSize.Width-10, this.ClientSize.Height - 40);
            pSkor.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pSkor);

            bTamam.SetBounds(this.ClientSize.Width - 85, this.ClientSize.Height -30, 80, 25);
            bTamam.Text = "Tamam";
            Controls.Add(bTamam);

            bVazgec.SetBounds(this.ClientSize.Width - 170, this.ClientSize.Height -30, 80, 25);
            bVazgec.Text = "Vazgeç";
            Controls.Add(bVazgec);

            lbtebrik.BorderStyle = BorderStyle.None;
            lbtebrik.TextAlign = ContentAlignment.MiddleCenter;
            lbtebrik.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lbtebrik.SetBounds(5 , 5, (pSkor.ClientSize.Width - 10), 30);
            lbtebrik.Text = "TEBRİKLER! YÜKSEK BİR SKOR YAPTINIZ!";
            lbtebrik.BackColor = Color.DarkOrange;
            lbtebrik.ForeColor = Color.White;
            pSkor.Controls.Add(lbtebrik);


            lbAd.BorderStyle = BorderStyle.None;
            lbAd.TextAlign = ContentAlignment.MiddleCenter;
            lbAd.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lbAd.SetBounds(5, 40, (pSkor.ClientSize.Width - 10), 30);
            lbAd.Text = "LÜTFEN ADINIZI GİRİN";
            lbAd.BackColor = Color.DarkSlateBlue;
            lbAd.ForeColor = Color.White;
            pSkor.Controls.Add(lbAd);

            lbSkorSaat.BorderStyle = BorderStyle.None;
            lbSkorSaat.TextAlign = ContentAlignment.MiddleCenter;
            lbSkorSaat.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lbSkorSaat.SetBounds(5, 75, (pSkor.ClientSize.Width - 10), 30);
            lbSkorSaat.Text = skor;
            lbSkorSaat.BackColor = pSkor.BackColor;
            lbSkorSaat.ForeColor = Color.Blue;
            pSkor.Controls.Add(lbSkorSaat);

            txtAd.BorderStyle = BorderStyle.FixedSingle;
            txtAd.TextAlign = HorizontalAlignment.Center;
            txtAd.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            txtAd.SetBounds(100, 110, (pSkor.ClientSize.Width - 200), 30);
            txtAd.Text = "İSİMSİZ";
            txtAd.ForeColor = Color.DeepSkyBlue;
            pSkor.Controls.Add(txtAd);

            bVazgec.Click += BVazgec_Click;
            bTamam.Click += BTamam_Click;



        }

        private void BTamam_Click(object sender, EventArgs e)
        {
            skorAdlar[4] = txtAd.Text.ToString();
            skorSureler[4] = yuksekSkor;

            for (int i = 4; i > 0; i--)
            {
                if (Int32.Parse(skorSureler[i].ToString()) > Int32.Parse(skorSureler[i - 1].ToString())) { 
                    String tempAd = skorAdlar[i - 1].ToString();
                    skorAdlar[i - 1] = skorAdlar[i];
                    skorAdlar[i] = tempAd;

                    String tempSure = skorSureler[i - 1].ToString();
                    skorSureler[i - 1] = skorSureler[i];
                    skorSureler[i] = tempSure;
                }


            }
            for (int i = 1; i <= 5; i++)
            {
                XDocument x = XDocument.Load(@"skorlar.xml");
                XElement node = x.Element("Skorlar").Elements("Skor").FirstOrDefault(a => a.Attribute("sira").Value.Trim().ToString() == (i).ToString());
                if (node != null)
                {
                    node.SetElementValue("Ad", skorAdlar[i - 1]);
                    node.SetElementValue("Sure", skorSureler[i - 1]);
                    x.Save(@"skorlar.xml");
                }

            }
            this.Close();
        }

        private void BVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
