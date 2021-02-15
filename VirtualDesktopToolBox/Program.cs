﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;

namespace VirtualDesktopToolBox
{
    static class Program
    {
        public static NotifyIcon Icon;
        public static ContextMenuStrip IconMenu;
        public static ToolStripMenuItem Pinitem;
        public static ToolStripMenuItem UnpinItem;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Icon = new NotifyIcon();
            Icon.Icon = System.Drawing.SystemIcons.Shield;
            Icon.Text = "VirtualDesktopToolBox";
            IconMenu = new ContextMenuStrip();
            Pinitem = new ToolStripMenuItem("Pin window");
            Pinitem.Click += Pinitem_Click;
            IconMenu.Items.Add(Pinitem);
            UnpinItem = new ToolStripMenuItem("Unpin window");
            UnpinItem.Click += UnpinItem_Click;
            IconMenu.Items.Add(UnpinItem);
            Icon.ContextMenuStrip = IconMenu;
            Icon.Visible = true;
            Application.Run();
        }

        private static void Pinitem_Click(object sender, EventArgs e)
        {
            WindowSelectDialog d = new WindowSelectDialog();
            d.ShowDialog();
            if (d.IsOK)
            {
                if (IsWindow(d.SelectedWindow))
                {
                    try
                    {
                        VirtualDesktop.PinWindow(d.SelectedWindow);
                        Icon.ShowBalloonTip(2000, "VirtualDesktopToolBox", "Pinned the selected window.", ToolTipIcon.Info);
                    }
                    catch(COMException)
                    {
                        Icon.ShowBalloonTip(2000, "VirtualDesktopToolBox", "The selected window is invalid.", ToolTipIcon.Error);
                    }
                }
                else
                {
                    Icon.ShowBalloonTip(2000, "VirtualDesktopToolBox", "The selected window is invalid.", ToolTipIcon.Error);
                }
            }
        }

        private static void UnpinItem_Click(object sender, EventArgs e)
        {
            WindowSelectDialog d = new WindowSelectDialog();
            d.ShowDialog();
            if (d.IsOK)
            {
                if (IsWindow(d.SelectedWindow))
                {
                    try
                    {
                        VirtualDesktop.UnpinWindow(d.SelectedWindow);
                        Icon.ShowBalloonTip(2000, "VirtualDesktopToolBox", "Unpinned the selected window.", ToolTipIcon.Info);
                    }
                    catch (COMException)
                    {
                        Icon.ShowBalloonTip(2000, "VirtualDesktopToolBox", "The selected window is invalid.", ToolTipIcon.Error);
                    }
                }
                else
                {
                    Icon.ShowBalloonTip(2000, "VirtualDesktopToolBox", "The selected window is invalid.", ToolTipIcon.Error);
                }
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindow(IntPtr hWnd);
    }
}
