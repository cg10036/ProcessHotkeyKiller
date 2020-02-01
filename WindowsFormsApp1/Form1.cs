using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, Keys vk);

        Keys FORCE_CLOSE = Keys.F4;
        Keys FORM_HIDE = Keys.F7;
        string PROCESS_NAME = "PROCESS_NAME"; //ex) chrome, iexplore, explorer ...

        bool hide_status = false;

        public Form1()
        {
            InitializeComponent();
            RegisterHotKey(Handle, 0, 0, FORCE_CLOSE);
            RegisterHotKey(Handle, 0, 0, FORM_HIDE);
            label1.Text += FORCE_CLOSE.ToString();
            label2.Text += FORM_HIDE.ToString();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x312:
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    if (key == FORCE_CLOSE)
                    {
                        var process_list = Process.GetProcessesByName(PROCESS_NAME);
                        for (int i = 0; i < process_list.Length; i++)
                        {
                            process_list[i].Kill();
                        }
                    }
                    else if (key == FORM_HIDE)
                    {
                        if(!hide_status)
                        {
                            this.Hide();
                        }
                        else
                        {
                            this.Show();
                        }
                        hide_status = !hide_status;
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
