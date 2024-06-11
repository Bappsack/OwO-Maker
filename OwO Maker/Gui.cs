using OwO_Maker.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static OwO_Maker.Helpers.Structs;
using static OwOMaker.Helpers.Mem;

namespace OwO_Maker
{
    public partial class Form1 : Form
    {

        private List<int> HWndList = new List<int>();
        private List<IntPtr> WindowList = new List<IntPtr>();
        private List<Thread> BotList = new List<Thread>();

        public bool Reset { get; private set; }
        public bool IsStarted { get; private set; }

        [DllImport("user32.dll")]
        static extern bool SetWindowText(IntPtr hWnd, string text);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern int EnumWindows(CallbackDef callback, int lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetClientRect(IntPtr hwnd, out RECT lpRect);

        private delegate bool CallbackDef(int hWnd, int lParam);

        private void RefreshHandle()
        {
            this.HWndList.Clear();
            this.listBox1.Items.Clear();
            CallbackDef callback = new CallbackDef(this.ShowWindowHandler);
            EnumWindows(callback, 0);
        }

        private bool ShowWindowHandler(int hWnd, int lParam)
        {
            StringBuilder stringBuilder = new StringBuilder(255);
            GetWindowText(hWnd, stringBuilder, 255);
            string text = stringBuilder.ToString();
            if (text.Contains("NosTale"))
            {
                GetWindowRect(hWnd, out var tempRect);
                if (tempRect.Right != 0 && tempRect.Bottom != 0)
                {
                    this.HWndList.Insert(0, hWnd);
                    this.listBox1.Items.Insert(0, "NosTale - (" + hWnd.ToString() + ")");
                    if (!Reset)
                        SetWindowText((IntPtr)hWnd, "NosTale - (" + hWnd.ToString() + ")");
                    else
                        SetWindowText((IntPtr)hWnd, "NosTale");
                }
            }

            if (this.listBox1.Items.Count > 0)
                this.listBox1.SelectedIndex = listBox1.Items.Count - 1;

            return true;
        }


        public Form1()
        {
            InitializeComponent();
            if (!Properties.Settings.Default.Disclaimer)
            {
                MessageBox.Show("DISCLAIMER:\n\n IF U PAID FOR THIS SOFTWARE U GOT SCAMMED!!!\n\n\n" +
                  "This Bot was made by Panda~ from Elitepvpers.com.\n\nNo Bot can gurantee u won't get banned for using Bots, so if u get banned for using this Software its your own fault!\n");
                Properties.Settings.Default.Disclaimer = true;
                Properties.Settings.Default.Save();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show($"This program must be started with Administrator privileges!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else
                this.Text = $"{this.Text} [ADMINISTRATOR]";

            LoadSettings();
            RefreshHandle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshHandle();
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();

            Reset = true;
            Program.botRunning = false;
            RefreshHandle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("No Client selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IntPtr ClientHWND = FindWindow("TNosTaleMainF", listBox1.SelectedItem.ToString());

            RECT rect;
            GetClientRect(ClientHWND, out rect);
            string Resolution = (rect.Right.ToString() + "x" + rect.Bottom.ToString());

            var buttons = ButtonResolutionHelper.GetButtonPositions(Resolution);

            if (buttons is null)
            {
                MessageBox.Show($"Game uses a unsupported Resolution ({Resolution})!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (WindowList.Contains(ClientHWND))
            {
                MessageBox.Show("This Client is already in the List!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!t_Level.Text.All(Char.IsDigit))
            {
                MessageBox.Show("Invalid Number for Level!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!t_FailChance.Text.All(Char.IsDigit))
            {
                MessageBox.Show("Invalid Number for Random Fail Min!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (t_FailChance.Text.All(Char.IsDigit))
            {
                var digit = Convert.ToInt32(t_FailChance.Text);
                if (digit < 0 || digit > 100)
                {
                    MessageBox.Show("Invalid Number for Random Fail Min!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (digit == 100)
                {
                    MessageBox.Show("a Fail Chance of 100 will result into failing everytime!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            WindowList.Add(ClientHWND);

            Minigame Game = GetWantedMinigame();

            string BotID = listBox1.SelectedItem.ToString().Replace("NosTale", "").Replace("- (", "").Replace(")", "").Replace(" ", "");
            string Title = listBox1.SelectedItem.ToString();
            int Amount = Convert.ToInt32(t_Times.Text);
            int Level = Convert.ToInt32(t_Level.Text);
            int failchance = Convert.ToInt32(t_FailChance.Text);
            uint prodkey = GetProdKey(ProductionCouponKey.Text);

            if (prodkey == 0)
            {
                MessageBox.Show("Invalid Production Key!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)Game == 0) { BotList.Add(new Thread(() => new Minigames.StoneQuarry().RunTask(FindWindow("TNosTaleMainF", Title), Amount, buttons, Convert.ToInt32(BotID), Level, HumanTime.Checked, ProductionCoupon.Checked, failchance, prodkey))); }
            if ((int)Game == 1) { BotList.Add(new Thread(() => new Minigames.SawMill().RunTask(FindWindow("TNosTaleMainF", Title), Amount, buttons, Convert.ToInt32(BotID), Level, HumanTime.Checked, ProductionCoupon.Checked, failchance, prodkey))); }
            if ((int)Game == 2) { BotList.Add(new Thread(() => new Minigames.ShootingRange().RunTask(FindWindow("TNosTaleMainF", Title), Amount, buttons, Convert.ToInt32(BotID), Level, HumanTime.Checked, ProductionCoupon.Checked, failchance, prodkey))); }
            if ((int)Game == 3) { BotList.Add(new Thread(() => new Minigames.FishPond().RunTask(FindWindow("TNosTaleMainF", Title), Amount, buttons, Convert.ToInt32(BotID), Level, HumanTime.Checked, ProductionCoupon.Checked, failchance, prodkey))); }

            string[] row = { BotID, GetWantedMinigame().ToString(), t_Level.Text, "0", "0", ProductionCoupon.Checked.ToString(), HumanTime.Checked.ToString(), $"0/{t_Times.Text}" };
            var bot = new ListViewItem(row);
            listView1.Items.Add(bot);

            MessageBox.Show($"{listBox1.SelectedItem.ToString()} added to the Bot List!\n" +
                $"\nMinigame: {Game}\nWanted Level: {t_Level.Text}\n" +
                $"Amount: {t_Times.Text}\n" +
                $"Human Time: {HumanTime.Checked.ToString()}\n" +
                $"Use Productions Coupon: {ProductionCoupon.Checked.ToString()}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SaveSettings();
        }

        private Minigame GetWantedMinigame()
        {
            int wantedGame = -1;
            if (StoneQuarry.Checked) { wantedGame = 0; }
            if (SawMill.Checked) { wantedGame = 1; }
            if (ShootingRange.Checked) { wantedGame = 2; }
            if (FishPond.Checked) { wantedGame = 3; }
            if (TypeWriter.Checked) { wantedGame = 4; }
            if (TypeWriter.Checked) { wantedGame = 5; }

            return (Minigame)Enum.Parse(typeof(Minigame), wantedGame.ToString(), true);

        }

        private uint GetProdKey(string text)
        {
            return text switch
            {
                "0" => (uint)BackgroundHelper.KeyCodes.VK_0,
                "1" => (uint)BackgroundHelper.KeyCodes.VK_1,
                "2" => (uint)BackgroundHelper.KeyCodes.VK_2,
                "3" => (uint)BackgroundHelper.KeyCodes.VK_3,
                "4" => (uint)BackgroundHelper.KeyCodes.VK_4,
                "5" => (uint)BackgroundHelper.KeyCodes.VK_5,
                "6" => (uint)BackgroundHelper.KeyCodes.VK_6,
                "7" => (uint)BackgroundHelper.KeyCodes.VK_7,
                "8" => (uint)BackgroundHelper.KeyCodes.VK_8,
                "9" => (uint)BackgroundHelper.KeyCodes.VK_9,
                _ => 0,
            };
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (BotList.Count == 0)
            {
                MessageBox.Show("No Bots Added!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!IsStarted)
            {
                MessageBox.Show("Bots aren't started yet!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int Amount = BotList.Count;
            foreach (Thread Bot in BotList)
                Bot.Interrupt();

            Program.botRunning = false;
            BotList.Clear();
            WindowList.Clear();
            listView1.Items.Clear();
            IsStarted = false;
            MessageBox.Show($"{Amount} Bots have been Stopped and removed from List!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (BotList.Count == 0)
            {
                MessageBox.Show("No Bots Added!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsStarted)
            {
                MessageBox.Show("Already Started!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Program.botRunning = true;

            Invoke(new Action(() =>
            {
                int Amount = BotList.Count;
                foreach (Thread Bot in BotList)
                {
                    Bot.Start();
                }
                MessageBox.Show($"{Amount} Bots have been started!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsStarted = true;
            }));


        }
        public void UpdateStatus(int botID, string game, int Level, int points, int prodPoints, bool UseProdCoupon, bool HumanTime, string progress)
        {
            Invoke(new Action(() =>
            {
                // Find item by botID
                var item = FindListViewItemByBotID(botID);
                string[] row = { botID.ToString(), game, Level.ToString(), points.ToString(), prodPoints.ToString(), UseProdCoupon.ToString(), HumanTime.ToString(), progress };
                if (item != null)
                    for (int i = 0; i < item.SubItems.Count; i++)
                        item.SubItems[i].Text = row[i].ToString();

            }));
        }

        private ListViewItem FindListViewItemByBotID(int BotID)
        {
            foreach (ListViewItem item in listView1.Items)
                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    if (subItem.Text == BotID.ToString())
                        return item;
            return null;
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.HumanTime = HumanTime.Checked;
            Properties.Settings.Default.UseProdCoupon = ProductionCoupon.Checked;

            if (t_FailChance.Text.All(Char.IsDigit) && Convert.ToInt32(t_FailChance.Text) <= 100 && Convert.ToInt32(t_FailChance.Text) > 0)
                Properties.Settings.Default.FailChance = t_FailChance.Text;


            if (ProductionCouponKey.Text.All(Char.IsDigit))
            {
                var check = Convert.ToInt32(ProductionCouponKey.Text);

                if (check >= 0 && check < 10)
                    Properties.Settings.Default.ProdKey = ProductionCouponKey.Text;
            }

            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            HumanTime.Checked = Properties.Settings.Default.HumanTime;
            ProductionCoupon.Checked = Properties.Settings.Default.UseProdCoupon;

            if (Properties.Settings.Default.FailChance.All(Char.IsDigit) && Convert.ToInt32(Properties.Settings.Default.FailChance) <= 100 && Convert.ToInt32(Properties.Settings.Default.FailChance) > 0)
                t_FailChance.Text = Properties.Settings.Default.FailChance;

            if (Properties.Settings.Default.ProdKey.All(Char.IsDigit))
            {
                var check = Convert.ToInt32(Properties.Settings.Default.ProdKey);

                if (check >= 0 && check < 10)
                    ProductionCouponKey.Text = Properties.Settings.Default.ProdKey;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.elitepvpers.com/forum/nostale-hacks-bots-cheats-exploits/4716766-OwO-maker-nostale-minigame-bot-source-poc.html");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void ProductionCouponKey_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
