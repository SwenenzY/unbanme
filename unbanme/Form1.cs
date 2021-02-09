using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.IO;

namespace unbanme
{
    public partial class Form1 : Form
    {
        private ChromeDriver ChromeDriver;
        private Thread BotThread;

        public Form1()
        {
            InitializeComponent();
        }

        void SetupSeleniumDriver()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            // options.AddArgument("--window-position=-32000,-32000");

            options.AddArgument("--lang=en-US");
            // options.AddArgument("headless");
            ChromeDriver = new ChromeDriver(service, options, TimeSpan.FromMinutes(5));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Starting bot");
            SetupSeleniumDriver();

            ChromeDriver.Url = "https://store.steampowered.com/login/?redir=%3Fl%3Dturkish&redir_ssl=1&snr=1_4_4__global-header";

            // username e bilgi
            System.Threading.Thread.Sleep(2500);
            ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[4]/div/div/div[1]/div/div/div/div/form/div[1]/input")).SendKeys(textBox1.Text);

            // password a bilgi
            System.Threading.Thread.Sleep(2500);
            ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[4]/div/div/div[1]/div/div/div/div/form/div[2]/input")).SendKeys(textBox2.Text);

            // login butonuna tıklama
            System.Threading.Thread.Sleep(2500);
            ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[4]/div/div/div[1]/div/div/div/div/div[3]/div[1]/button")).Click();

            System.Threading.Thread.Sleep(5000); // login için 5 sn bekle
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(BotThread != null)
            {
                BotThread.Abort();
                BotThread = null;
            }

            BotThread = new Thread(ChangeNameAndClearalis)
            {
                IsBackground = true,
                Priority = ThreadPriority.Highest
            };
            if (!(BotThread.ThreadState == System.Threading.ThreadState.Running))
            {
                BotThread.Start();
            }
        }
        private readonly Random _random = new Random();
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;
            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        void ChangeNameAndClearalis()
        {
            while(true)
            {
                ChromeDriver.Url = "http://steamcommunity.com/my/profile"; // profile giriş

                Thread.Sleep(10000);
              
                try
                {
                    // alt butona ekleme
                    System.Threading.Thread.Sleep(2500);
                    ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[6]/div[1]/div[1]/div/div/div/div[1]/div[1]/span[2]")).Click();

                    // Geçmiş isimileri temizleme
                    System.Threading.Thread.Sleep(2500);
                    ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[6]/div[1]/div[1]/div/div/div/div[1]/div[1]/div/div/div[3]/a")).Click();

                    // tamam a tıklama
                    System.Threading.Thread.Sleep(2500);
                    ChromeDriver.FindElement(By.XPath("/html/body/div[4]/div[3]/div/div[2]/div[1]/span")).Click();
                }
                catch (Exception hata)
                {

                }

                ChromeDriver.Url = "http://steamcommunity.com/my/profile"; // profile giriş


                System.Threading.Thread.Sleep(3000); // profil için 3 sn bekle

                try
                {
                    // edit profile a tıklama 
                    System.Threading.Thread.Sleep(2500);
                    ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[6]/div[1]/div[1]/div/div/div/div[3]/div[2]/a/span")).Click();


                    // name kutucuğu 
                    System.Threading.Thread.Sleep(2500);
                    ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[3]/div/div[2]/div/div/div[3]/div[2]/div[2]/form/div[3]/div[2]/div[1]/label/div[2]/input")).Clear();


                    int num4 = new Random().Next(1, File.ReadAllLines("Names.txt").Count<string>());
                    string username = File.ReadAllLines("Names.txt")[num4]; // "Kul" + (TotalAccount + 1);//


                    // isim girme
                    System.Threading.Thread.Sleep(2500);
                    ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[3]/div/div[2]/div/div/div[3]/div[2]/div[2]/form/div[3]/div[2]/div[1]/label/div[2]/input")).SendKeys(username);

                    // url temizleme
                    System.Threading.Thread.Sleep(2500);
                    ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[3]/div/div[2]/div/div/div[3]/div[2]/div[2]/form/div[3]/div[2]/div[3]/label/div[2]/input")).Clear();

                    // url isim girme
                    string CUrl = RandomString(20,true);
                    System.Threading.Thread.Sleep(2500);
                    ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[3]/div/div[2]/div/div/div[3]/div[2]/div[2]/form/div[3]/div[2]/div[3]/label/div[2]/input")).SendKeys(CUrl);

                    // tammama tıklama 
                    ChromeDriver.FindElement(By.XPath("/html/body/div[1]/div[7]/div[3]/div/div[2]/div/div/div[3]/div[2]/div[2]/form/div[7]/button[1]")).Click();
                    
                    listBox1.Items.Add("Name changed to -> " + username + " and cleared alias");

                }
                catch (Exception hatam)
                {

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (BotThread != null)
            {
                BotThread.Abort();
                BotThread = null;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
