using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NDPOdev
{
    class AnaPencere : Form
    {
        XmlTextWriter createXML;
        XmlTextReader readXML;
        SoundPlayer topKnock;
        SoundPlayer gameOver;
        DateTime basZamani;
        String skor;
        Panel mAraclar;
        Button mYSkorlar;
        Label mSaat;
        Label mBilgi;
        Top top;
        Raket raket;
        ArrayList skorAdlar;
        ArrayList skorSureler;
        Timer timer1;
        Timer timer2;
        public AnaPencere(int width, int height)
        {
            String path = Application.StartupPath;
            if (!File.Exists("skorlar.xml")) {

                createXML = new XmlTextWriter("skorlar.xml", UTF8Encoding.UTF8);
                createXML.WriteStartDocument();
                createXML.WriteStartElement("Skorlar");
                createXML.WriteEndDocument();
                createXML.Close();
                for(int i = 0; i < 5; i++)
                {
                    XmlDocument _data = new XmlDocument();
                    _data.Load("skorlar.xml");

                    XmlElement _element = _data.CreateElement("Skor");
                    _element.SetAttribute("sira", (i+1).ToString());

                    XmlElement _ad = _data.CreateElement("Ad");
                    _ad.InnerText = "İSİMSİZ";
                    _element.AppendChild(_ad);

                    XmlElement _sure = _data.CreateElement("Sure");
                    _sure.InnerText = "0000000";
                    _element.AppendChild(_sure);
                    _data.DocumentElement.AppendChild(_element);

                    XmlTextWriter _write = new XmlTextWriter("skorlar.xml", null);
                    _write.Formatting = Formatting.Indented;
                    _data.WriteContentTo(_write);
                    _write.Close();
                }
            }
            xmlOku();


            path = path.Substring(0, path.LastIndexOf('\\'));
            path = path.Substring(0, path.Length - 3);
            gameOver = new SoundPlayer(path + "Sounds\\pacman_death.wav");
            topKnock = new SoundPlayer(path + "Sounds\\doorKnock5.wav");
            mAraclar = new Panel();
            mYSkorlar = new Button();
            mYSkorlar.Click += MYSkorlar_Click;
            mSaat = new Label();
            mBilgi = new Label();
            skor = "00:00:000";
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            SetBounds((Screen.GetBounds(this).Width / 2) - (width / 2), (Screen.GetBounds(this).Height / 2) - (height / 2), width, height);
            Text = "Arcanoid";
            raket = new Raket(AnaPencere.MousePosition.X - 100, this.ClientSize.Height - 20);
            top = new Top(0, 0, 0);
            timer1 = new Timer();
            timer1.Interval = 1;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2 = new Timer();
            timer1.Interval = 1;
            timer2.Tick += new EventHandler(timer2_Tick);
            Paint += new PaintEventHandler(AnaPencere_Paint);
            MouseClick += new MouseEventHandler(AnaPencere_MouseClick);
            MouseMove += new MouseEventHandler(AnaPencere_MouseMove);
            timer1.Start();
        }

        private void MYSkorlar_Click(object sender, EventArgs e)
        {
            xmlOku();
            YuksekSkor frmYuksekSkor = new YuksekSkor(400, 180,skorAdlar,skorSureler);
            frmYuksekSkor.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            skor = String.Format("{0:00}:{1:00}:{2:000}", (DateTime.Now - basZamani).Minutes, (DateTime.Now - basZamani).Seconds, (DateTime.Now - basZamani).Milliseconds);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!top.carpismaVarMi(new Vektor(0.0, this.ClientSize.Height - top.YariCap), new Vektor(this.ClientSize.Width, this.ClientSize.Height - top.YariCap)))
            {
                if (top.carpismaVarMi(new Vektor(this.ClientSize.Width, 0.0), new Vektor(this.ClientSize.Width, this.ClientSize.Height)))
                {
                    topKnock.Play();
                    top.yansima("YAN");
                }
                if (top.carpismaVarMi(new Vektor(0.0, 0.0), new Vektor(0.0, this.ClientSize.Height)))
                {
                    topKnock.Play();
                    top.yansima("YAN");
                }
                if (top.carpismaVarMi(new Vektor(0.0, 50.0), new Vektor(this.ClientSize.Width, 50.0)))
                {
                    topKnock.Play();
                    top.yansima("UST");
                }
                if (raket.carpismaVarMi(top, this.ClientSize.Height))
                {
                    topKnock.Play();
                    top.Yon = top.Yon + top.Yon / 10.0;
                    top.yansima("UST");
                }
                top.hareketEttir();
                Invalidate();
            }
            else
            {
                skor = String.Format("{0:00}:{1:00}:{2:000}", (DateTime.Now - basZamani).Minutes, (DateTime.Now - basZamani).Seconds, (DateTime.Now - basZamani).Milliseconds);
                timer1.Stop();
                timer2.Stop();
                gameOver.Play();
                foreach (string str in skorSureler)
                {

                    if (Int32.Parse(skor.Replace(@":", string.Empty))> Int32.Parse(str))
                    {
                        
                        SkorKayit frmSkorKayit = new SkorKayit(450, 230, skor,skorSureler,skorAdlar);
                        frmSkorKayit.Show();
                        break;
                    }
                    
                    
                }

            }



        }

        public void AnaPencere_MouseMove(object sender, MouseEventArgs e)
        {

            if (this.ClientSize.Width - e.X < 100)
            {
                raket = new Raket(this.ClientSize.Width - 200, this.ClientSize.Height - 20);
            }
            else if (e.X - 0 < 100)
            {
                raket = new Raket(0, this.ClientSize.Height - 20);
            }
            else
            {
                raket = new Raket(e.X - 100, this.ClientSize.Height - 20);
            }
            
        }

        private void AnaPencere_MouseClick(object sender, MouseEventArgs e)
        {
            timer1.Start();
            timer2.Start();
            basZamani = DateTime.Now;
            topOlustur(e.X - 10);
        }

        private void AnaPencere_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.DarkBlue, 0, 0, this.ClientSize.Width, 50);
            formNesneleriOlustur();
            e.Graphics.FillRectangle(Brushes.DarkSlateBlue, 0, 50, this.ClientSize.Width, this.ClientSize.Height);
            top.ciz(e.Graphics);
            raket.ciz(e.Graphics);
        }

        private void topOlustur(int x)
        {
            top = new Top(x, this.ClientSize.Height - 50, 15);
        }
        private void formNesneleriOlustur()
        {
            mAraclar.SetBounds(0, 0, this.ClientSize.Width, 50);
            mAraclar.BorderStyle = BorderStyle.None;
            mAraclar.BackColor = Color.DarkBlue;
            Controls.Add(mAraclar);

            mSaat.Font = new Font("Microsoft Sans Serif", 17F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            mSaat.ForeColor = Color.Orange;
            mSaat.SetBounds(13, 10, 76, 30);
            mSaat.Text = skor;
            mSaat.BackColor = Color.DarkBlue;
            mSaat.BorderStyle = BorderStyle.None;
            mAraclar.Controls.Add(mSaat);


            mYSkorlar.FlatStyle = FlatStyle.Flat;
            mYSkorlar.Text = "Yüksek Skorlar";
            mYSkorlar.ForeColor = Color.Orange;
            mYSkorlar.Font = new Font("Microsoft Sans Serif", 7.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            mYSkorlar.SetBounds(100, 10, 100, 30);
            mYSkorlar.BackColor = Color.Indigo;
            mAraclar.Controls.Add(mYSkorlar);

            mBilgi.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            mBilgi.SetBounds(400, 10, this.ClientSize.Width - 400, 30);
            mBilgi.TextAlign = ContentAlignment.MiddleRight;
            mBilgi.ForeColor = Color.LightSkyBlue;
            mBilgi.Text = "Yeni oyun başlatmak için tek tıklayın.";
            mBilgi.BackColor = Color.DarkBlue;
            mBilgi.BorderStyle = BorderStyle.None;
            mAraclar.Controls.Add(mBilgi);
        }

        private void xmlOku()
        {

            readXML = new XmlTextReader("skorlar.xml");
            skorAdlar = new ArrayList(5);
            skorSureler = new ArrayList(5);
            try
            {
                while (readXML.Read()) //Dosyadaki veriler tükenene kadar okuma işlemi devam eder.
                {
                    if (readXML.NodeType == XmlNodeType.Element)//Düğümlerdeki veri element türünde ise okuma gerçekleşir.
                    {
                        switch (readXML.Name)//Elementlerin isimlerine göre okuma işlemi gerçekleşir.
                        {
                            case "Ad":
                                skorAdlar.Add(readXML.ReadString());
                                break;
                            case "Sure":
                                skorSureler.Add(readXML.ReadString());
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Xml Bağlantı Hatası : " + ex.Message);
            }

            readXML.Close();
        }


    }
}
