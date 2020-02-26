using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Gaming.Input;

namespace xboxController
{
    public partial class Form1 : Form
    {
        Gamepad Controller;
        Timer t = new Timer();
        


        public Form1()
        {
            
            InitializeComponent();
            serialPort1.Open();
            openit("http://192.168.43.48:8090/test.mjpg");
            t.Tick += T_Tick;
            t.Interval = 50;
            t.Start();
            
        }
        private void T_Tick(object sender, EventArgs e)
        {
            if(Gamepad.Gamepads.Count > 0)
            {
                Controller = Gamepad.Gamepads.First();
                var Reading = Controller.GetCurrentReading();
                double LY = -Reading.LeftThumbstickY;
                //double LX = Reading.LeftThumbstickX;
                double RY = -Reading.RightThumbstickY;
                //double RX = Reading.RightThumbstickX;
                //double RT = Reading.RightTrigger;
                //double LT = Reading.LeftTrigger;
                serialPort1.Write("L" + RY.ToString("0.00"));
                serialPort1.Write("R" + LY.ToString("0.00"));
                
                
                switch (Reading.Buttons)
                {
                    case GamepadButtons.A:
                        serialPort1.Write("A");
                        break;
                    case GamepadButtons.B:
                        serialPort1.Write("B");
                        break;

                }
                    Debug.WriteLine("R" + LY.ToString("0.00"));
                    Debug.WriteLine("L" + RY.ToString("0.00"));

            


            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static void openit(string x)
        {
            System.Diagnostics.Process.Start("cmd", "/C start" + " " + x);
        }
    }
}
