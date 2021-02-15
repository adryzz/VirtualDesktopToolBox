using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;

namespace VirtualDesktopToolBox
{
    public partial class WindowSelectDialog : Form
    {
        public bool IsOK = false;
        public IntPtr SelectedWindow = IntPtr.Zero;
        public WindowSelectDialog()
        {
            InitializeComponent();
        }

        private void WindowSelectDialog_Load(object sender, EventArgs e)
        {
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        private void WindowSelectDialog_Shown(object sender, EventArgs e)
        {
            VirtualDesktop.PinWindow(Handle);
            System.Threading.Thread.Sleep(2000);
            SelectedWindow = GetForegroundWindow();
            IsOK = true;
            Close();
        }
    }
}
