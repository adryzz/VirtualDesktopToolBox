using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;

namespace VirtualDesktopToolBox
{
    public partial class ExtraItemsWindow : Form
    {
        public ExtraItemsWindow()
        {
            InitializeComponent();
        }

        private void ExtraItemsWindow_Load(object sender, EventArgs e)
        {
            VirtualDesktop.Created += VirtualDesktop_Created;
            VirtualDesktop.Destroyed += VirtualDesktop_Destroyed;
            VirtualDesktop.CurrentChanged += VirtualDesktop_CurrentChanged;
            Invoke(new DesktopReloader(ReloadDesktops));
        }

        private void VirtualDesktop_Created(object sender, VirtualDesktop e)
        {
            Invoke(new DesktopReloader(ReloadDesktops));
        }

        private void VirtualDesktop_Destroyed(object sender, VirtualDesktopDestroyEventArgs e)
        {
            Invoke(new DesktopReloader(ReloadDesktops));
        }

        private void VirtualDesktop_CurrentChanged(object sender, VirtualDesktopChangedEventArgs e)
        {
            
        }

        delegate void DesktopReloader();

        void ReloadDesktops()
        {
            listView1.Items.Clear();
            List<VirtualDesktop> v = VirtualDesktop.GetDesktops().ToList();
            for (int i = 0; i < v.Count; i++)
            {
                listView1.Items.Add($"Desktop #{i}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VirtualDesktop.Create();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<VirtualDesktop> v = VirtualDesktop.GetDesktops().ToList();
            var indices = listView1.CheckedIndices;
            foreach (int index in indices)
            {
                v[index].Remove();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<VirtualDesktop> v = VirtualDesktop.GetDesktops().ToList();
            foreach(VirtualDesktop d in v)
            {
                d.Remove();
            }
        }
    }
}
