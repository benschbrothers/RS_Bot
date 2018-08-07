using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Diagnostics;

using System.Runtime.InteropServices;
using System.Windows.Interop.HwndSource;
using System.IO;

//###########################################
//#                                         #
//#     BotV3 By Chris                      #
//#     CP by TaskManager91                 #
//#                                         #
//###########################################


namespace BOTV3
{
    public partial class MainWindow : Window
    {
        // Fenster über das Handle aktivieren
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private HwndSource _source;
        private const int HOTKEY_ID = 9000;

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        Process[] blaProcess = Process.GetProcessesByName("bla"); //you know what todo here ;)
        IntPtr RSWindow;

        int xFix = 0;
        int yFix = 0;
        int ln = 0;
        int ore = 0;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var helper = new WindowInteropHelper(this);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);
            RegisterHotKey();
        }

        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            _source = null;
            UnregisterHotKey();
            base.OnClosed(e);
        }

        private void RegisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            const uint VK_F10 = 0x79;
            const uint MOD_CTRL = 0x0002;
            if (!RegisterHotKey(helper.Handle, HOTKEY_ID, MOD_CTRL, VK_F10))
            {
                // handle error
            }
        }

        private void UnregisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            OnHotKeyPressed();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void OnHotKeyPressed()
        {
            // do stuff
        }

        public MainWindow()
        {
            
            InitializeComponent();
            //dragMouse(1733, 380, 1500, 500);

        }

        public void schmieden()
        {
            System.Threading.Thread.Sleep(2000);

            for (int i = 0; i <= 100; i++)
            {
                rightClick(700, 425);
                System.Threading.Thread.Sleep(500);
                mouseClick(660, 472);

                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait("{2}");
                System.Threading.Thread.Sleep(200);
                SendKeys.SendWait("{0}");
                System.Threading.Thread.Sleep(200);
                SendKeys.SendWait("{ENTER}");

                RGB px1 = getPixel(564, 546);
                moveMouse(360, 510);
                System.Threading.Thread.Sleep(500);
                RGB px2 = getPixel(514, 556);
                if (px1.getG() == 255 && px1.getB() == 255)
                {
                    mouseClick(410, 500);
                }
                else if (px2.getG() == 255 && px2.getB() == 255)
                {
                    mouseClick(360, 510);
                }
                else
                {
                    Console.Out.WriteLine("**** up reset and restart");
                }
            }
        }

        public void mithrilFarmen()
        {
            int erze = 0;
            Boolean empInv = true;

            for (int i = 0; i <= 100; i++)
            {
                while (empInv)
                {
                    mouseClick(1723, 346);
                    System.Threading.Thread.Sleep(3000);
                    mine(960, 580);

                    empInv = checkInv();
                }
                erze += 28;

                Console.Out.WriteLine("Run Nummer:" + i + " Erze bis jetzt:" + erze);
                writeLog(" " + erze + " Mithril gefarmt");

                System.Threading.Thread.Sleep(9000);
                empInv = true;
                miningBank();
            }
        }

        public void kohleFarmen()
        {
            int erze = 0;
            Boolean empInv = true;
            for (int i = 0; i <= 200; i++)
            {
                mouseClick(1785, 335);
                System.Threading.Thread.Sleep(9000);
                
                while (empInv)
                {
                    moveMouse(900, 620);
                    System.Threading.Thread.Sleep(1000);
                    RGB PX = getPixel(970, 660);
                    if (PX.getR() > 0 && PX.getG() > 0)
                    {
                        Console.Out.Write("Fixing Position");
                        mouseClick(1714, 340);
                        System.Threading.Thread.Sleep(1500);
                    }
                    
                    mine(960, 670);

                    mouseClick(1693, 344);
                    System.Threading.Thread.Sleep(3000);
                    empInv = checkInv();
                }
                erze += 28;
                 
                Console.Out.WriteLine("Run Nummer:" + i + " Erze bis jetzt:" + erze);
                writeLog(" "+erze +" Kohle gefarmt");

                miningBank();
                empInv = true;
            }
        }

        public void miningBank()
        {
            if (ln == 0)
            {
                //öffne Bank 
                moveMouse(960, 550);
                System.Threading.Thread.Sleep(500);
                RGB px1 = getPixel(1137, 595);
                moveMouse(900, 590);
                System.Threading.Thread.Sleep(500);
                RGB px2 = getPixel(1077, 635);
                if (px1.getG() == 255 && px1.getB() == 255)
                {
                    mouseClick(960, 550);
                }
                else if (px2.getG() == 255 && px2.getB() == 255)
                {
                    mouseClick(900, 590);
                }
                else
                {
                    Console.Out.WriteLine("**** up reset and restart");
                    writeLog("error");
                    Close();
                }
            }
            else
            {
                moveMouse(960, 550);
                System.Threading.Thread.Sleep(500);
                RGB PX = getPixel(1053, 595);
                moveMouse(900, 590);
                System.Threading.Thread.Sleep(500);
                RGB px2 = getPixel(993, 635);
                if (PX.getR() == 0)
                {
                    System.Threading.Thread.Sleep(500);
                    mouseClick(960, 560);
                }
                else if(PX.getR() == 0)
                {
                    System.Threading.Thread.Sleep(500);
                    mouseClick(900, 580);
                }
                else
                {
                    Console.Out.WriteLine("**** up reset and restart");
                    writeLog("error");
                    Close();
                }
            }
            System.Threading.Thread.Sleep(2000);
            mouseClick(1135, 490);
            System.Threading.Thread.Sleep(500);
        }

        public Boolean checkInv()
        {
            RGB px = getPixel(1806, 891);
            Boolean res = false;

            if (px.getR() == 8 && px.getG() == 26 && px.getB() == 36)
            {
                res = true;
            }
            return res;
        }

        public void mine(int xpos, int ypos)
        {
            int time = 0;
            int waiting = 40;
            moveMouse(xpos, ypos);
            System.Threading.Thread.Sleep(500);
            RGB PX = getPixel(xpos + xFix, ypos + yFix);
            if (checkInv() == true)
            {
                while (PX.getG() != 255 && PX.getB() != 255 && time <= waiting)
                {
                    System.Threading.Thread.Sleep(500);
                    PX = getPixel(xpos + xFix, ypos + yFix);
                    time += 1;
                }
                if (PX.getG() == 255 && PX.getB() == 255 && time <= waiting)
                {
                    mouseClick(xpos, ypos);
                }
                while (PX.getG() == 255 && PX.getB() == 255 && time <= waiting)
                {
                    System.Threading.Thread.Sleep(500);
                    PX = getPixel(xpos + xFix, ypos + yFix);
                }
            }
        }

        public void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Key.P)
            {
                Console.Out.WriteLine("P Pressed");
            }
        }

        public void moveMouse(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
        }

        public void mouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        public void rightClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, xpos, ypos, 0, 0);
        }

        public void dragMouse(int x1, int y1, int x2, int y2)
        {
            SetCursorPos(x1, y1);
            mouse_event(MOUSEEVENTF_LEFTDOWN, x1, y1, 0, 0);
            SetCursorPos(x2, y2);
            mouse_event(MOUSEEVENTF_LEFTDOWN, x2, y2, 0, 0);
            System.Threading.Thread.Sleep(400);
            mouse_event(MOUSEEVENTF_LEFTUP, x2, y2, 0, 0);
        }

        public RGB getPixel(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            RGB px = new RGB((int)(pixel & 0x000000FF),
            (int)(pixel & 0x0000FF00) >> 8,
            (int)(pixel & 0x00FF0000) >> 16);
            return px;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((bool)Deutsch.IsChecked)
            {
                ln = 0;
                if ((bool)Kohle.IsChecked)
                {
                    Console.Out.WriteLine("D+K");
                    ore = 0;
                    xFix = 166;
                    yFix = 47;
                }
                else
                {
                    Console.Out.WriteLine("D+M");
                    ore = 1;
                    xFix = 180;
                    yFix = 45;
                }
            }
            else
            {
                ln = 1;
                if ((bool)Kohle.IsChecked)
                {
                    Console.Out.WriteLine("E+K");
                    ore = 0;
                    xFix = 139;
                    yFix = 46;
                }
                else
                {
                    Console.Out.WriteLine("E+M");
                    ore = 1;
                    xFix = 180;
                    yFix = 45;
                }
            }

            foreach (Process p in blaProcess)
            {
                RSWindow = p.MainWindowHandle;
                Console.Out.WriteLine(p.ProcessName);
                SetForegroundWindow(RSWindow);
            }
            writeLog(" started");
            
            if (ore == 0)
                kohleFarmen();
            else
                mithrilFarmen();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (Process p in blaProcess)
            {
                RSWindow = p.MainWindowHandle;
                Console.Out.WriteLine(p.ProcessName);
                SetForegroundWindow(RSWindow);
            }
            schmieden();
        }

        public void writeLog(String input)
        {
            String text = "Chris: " + input;
            System.IO.File.WriteAllText(@"\\SERVER\www\bla", text);
        }
    }

    public class RGB
    {
        int r;
        int g;
        int b;
        public RGB(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public int getR() { return r; }
        public int getG() { return g; }
        public int getB() { return b; }
    }
}
