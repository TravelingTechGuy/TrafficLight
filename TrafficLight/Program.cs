using System;
using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace TrafficLight
{
    public class Program
    {
        private static bool on = false;
        private static Thread thread = new Thread(TrafficLight);
        private static LED red = new LED(Pins.GPIO_PIN_D11);
        private static LED yellow = new LED(Pins.GPIO_PIN_D12);
        private static LED green = new LED(Pins.GPIO_PIN_D13);

        public static void Main()
        {
            // write your code here
            InterruptPort button = new InterruptPort(Pins.ONBOARD_SW1, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeHigh);
            button.OnInterrupt += new NativeEventHandler(button_OnInterrupt);

            Thread.Sleep(Timeout.Infinite);
        }

        private static void TrafficLight()
        {
            while (true)
            {
                red.Dash();
                yellow.Dash();
                green.Dash();
            }
        }

        private static void AllOff()
        {
            red.Off();
            yellow.Off();
            green.Off();
        }

        static void button_OnInterrupt(uint data1, uint data2, DateTime time)
        {
            if(data2 != 0)
                on = !on;
            if (on)
            {
                if(thread.ThreadState == ThreadState.Unstarted)
                    thread.Start();
                else
                    thread.Resume();
            }
            else
            {
                thread.Suspend();
                AllOff();
            }
        }
    }
}
