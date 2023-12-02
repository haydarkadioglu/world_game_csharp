using System.Text;

namespace adam_asmaca
{
    public partial class Form1 : Form
    {
        private List<string> kelimeler = new List<string>();
        private string dosyaYolu = @"C:\Users\ahayd\source\repos\adam_asmaca\adam_asmaca\words.txt", rastgeleKelime;
        private List<char> harf = new List<char>();
        private List<string> harf2 = new List<string>();
        private int indexLetter, uzunluk, NumOfRight = 7;
        

        public Form1()
        {
            InitializeComponent();

            deactive();
            button33.Visible = false;
        }
        private void deactive2(Button value)
        {
            value.Enabled = false;
            value.BackColor = Color.Red;
        }

        private List<Button> GetButtonList()
        {
            List<Button> buttonList = new List<Button>();

            for (int i = 1; i <= 31; i++)
            {
                string buttonName = "button" + i.ToString();
                Control[] foundButtons = this.Controls.Find(buttonName, true);

                if (foundButtons.Length > 0 && foundButtons[0] is Button)
                {
                    buttonList.Add((Button)foundButtons[0]);
                }
            }

            return buttonList;
        }
        private void active2()
        {
            List<Button> buttonList = GetButtonList();
            foreach (Button item in buttonList)
            {
                item.Enabled = true;
                item.BackColor = Color.PaleTurquoise;

            }

        }
        private void deactive()
        {
            textBox1.Visible = false;
            button32.Visible = false;
            button33.Visible = true;
        }
        private void active()
        {
            textBox1.Visible = true;
            button32.Visible = true;
            button33.Visible = false;
        }


        private void getWordList()
        {
            if (kelimeler.Count == 0)
            {
                using (StreamReader sr = new StreamReader(dosyaYolu))
                {
                    string satir;

                    while ((satir = sr.ReadLine()) != null)
                    {
                        kelimeler.Add(satir.Trim());
                    }
                }
                return;
            }
            else
            {
                return;
            }
        }
        private void getWord()
        {

            label2.Text = "";



            if (kelimeler.Count > 0)
            {
                Random random = new Random();
                int rastgeleIndex = random.Next(0, kelimeler.Count);
                rastgeleKelime = kelimeler[rastgeleIndex];

                uzunluk = rastgeleKelime.Length;
                for (int i = 0; i < uzunluk; i++)
                {
                    
                    harf2.Add(" _");
                    label2.Text += harf2[i];
                }
                
                harf = rastgeleKelime.ToCharArray().ToList();
                
            }
            else
            {
                getWordList();
            }
        }
        private void writeAlp(int indis)
        {
            bool cont = false;
            string textchanged;
            List<string> harfler = new List<string>
            {
                "a", "b", "c", "ç", "d", "e", "f", "g", "ğ", "h", "ı", "i", "j", "k", "l", "m",
                "n", "o", "ö", "p", "r", "s", "ş", "t", "u", "ü", "v", "w", "x", "y", "z"
            };

            foreach (var item in harf)
            {
                if (harfler[indis] == item.ToString())
                {
                    cont = true;
                    //label2.Text += harfler[indis];
                    indexLetter = harf.IndexOf(item);
                    harf2[indexLetter] = harfler[indis];
                    for (int i = indexLetter+1; i<uzunluk; i++)
                    {
                        if (harf[i].ToString() == harfler[indis])
                        {
                            harf2[i] = harfler[indis];

                        }
                    }
                    label2.Text = "";
                    for (int i = 0; i < uzunluk; i++)
                    {

                        harf2.Add(" _");
                        label2.Text += harf2[i];
                        
                    }
                    
                }
                
            }
            if (cont == false)
            {
                NumOfRight--;
                label1.Text = $"hak sayısı = {NumOfRight}";
                if (NumOfRight == 0)
                {
                    MessageBox.Show($"hakkınız bitti\nDoğru kelime ''{rastgeleKelime}''");
                    Clear();
                }
            }

            


        }

        private void Clear()
        {
            label2.Text = "";
            harf2.Clear();
            harf.Clear();
            NumOfRight = 7;
            label1.Text = $"hak sayısı = {NumOfRight}";
            getWord();
            active2();
        }


        private void CommonButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {

                int buttonIndex = int.Parse(clickedButton.Name.Substring(6));
                writeAlp(buttonIndex - 1);

            }
            deactive2(clickedButton);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                deactive();
            }
            else if (radioButton2.Checked)
            {
                active();
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Clear();
            
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string yeniKelime = textBox1.Text;
            kelimeler.Add(yeniKelime);
            textBox1.Text = "";
            File.AppendAllText(dosyaYolu, yeniKelime + Environment.NewLine);
        }
    }
}
