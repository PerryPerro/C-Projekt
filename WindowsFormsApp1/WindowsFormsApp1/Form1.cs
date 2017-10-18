using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        public string xml = "";
        private List<string> pods = new List<string>();
        WMPLib.WindowsMediaPlayer Player;
        public Form1()
        {
            InitializeComponent();
           
        }
        
        public Dictionary<String, String> categorys = new Dictionary<String, String>();
        public List<KeyValuePair<String, String>> kategorier = new List<KeyValuePair<String, String>>();

        public List<string> Pods { get => Pods1; set => Pods1 = value; }
        public List<string> Pods1 { get => pods; set => pods = value; }
        public List<string> Pods2 { get => pods; set => pods = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            

            //Ladda hem XML.
            
            using (var client = new System.Net.WebClient())
            {
                client.Encoding = Encoding.UTF8;
                xml = client.DownloadString(textBox1.Text); 
            }

            //Skapa en objektrepresentation.
            var dom = new System.Xml.XmlDocument();
            dom.LoadXml(xml);

            //Iterera igenom elementet item.
            foreach (System.Xml.XmlNode item
               in dom.DocumentElement.SelectNodes("channel/item"))
            {
                //Skriv ut dess titel.
                var title = item.SelectSingleNode("title");
                Pods.Add(item.SelectSingleNode("title").InnerText);
                listBox1.Text = title.InnerText;
                Console.WriteLine(title.InnerText);
            }
            for(int i =0; i < Pods.Count; i++)
            {
                listBox1.Items.Add(Pods.ElementAt(i));

            }
           
        }
        private void visaLista()
        {
            var valtItem = comboBox1.SelectedItem.ToString();
            listBox2.Items.Add(categorys.ContainsKey(valtItem));
        }
        public void setCategory(string category)
        {
            category = textBox2.Text.ToString();
            var choosenPod = listBox1.SelectedItem.ToString();
            kategorier.Add(new KeyValuePair<String, String>(category, choosenPod));
            //varför inte katergorier.Key?
            var item = new KeyValuePair<String, String>(category, choosenPod);
            if (comboBox1.Items.Contains(item.Key) == false) {
                comboBox1.Items.Add(item.Key);
            }
            
         
        }
        private void fillListWithObjects()
        {
            listBox2.Items.Clear();
 
            foreach (var item in kategorier)
            {
                if (comboBox1.SelectedItem.ToString() == item.Key)
                {
                    listBox2.Items.Add(item.Value);
                }
                
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            setCategory(textBox2.Text);
            comboBox1.Text = textBox2.Text;
    
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fillListWithObjects();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var client = new System.Net.WebClient())
            {
                client.Encoding = Encoding.UTF8;
                xml = client.DownloadString(textBox1.Text);
            }

            //Skapa en objektrepresentation.
            var dom = new System.Xml.XmlDocument();
            dom.LoadXml(xml);

            //Iterera igenom elementet item.
            foreach (System.Xml.XmlNode item
               in dom.DocumentElement.SelectNodes("channel/item"))
            {
                if (item.SelectSingleNode("title").InnerText == listBox2.SelectedItem.ToString())
                {
                    MessageBox.Show(item.SelectSingleNode("description").InnerText);
                }
                //Skriv ut dess titel.
                
                
                
            }
            for (int i = 0; i < Pods.Count; i++)
            {
                listBox1.Items.Add(Pods.ElementAt(i));

            }
        }
        private void PlayFile(String url)
        {
            Player = new WMPLib.WindowsMediaPlayer();
            Player.URL = url;
            Player.controls.play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PlayFile(@"C:\Users\Andreas\Downloads\Hejsan.mp3");

            //WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

            //wplayer.URL = @"C:\Users\AccExD\Downloads\test.mp3";
            //wplayer.controls.play();

        }
    }
}
