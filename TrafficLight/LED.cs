using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using System.Threading;

namespace TrafficLight
{
    class LED
    {
        //the LED
        private OutputPort led;
        
        public LED(Cpu.Pin pin)
        {
            led = new OutputPort(pin, false);
        }

        public void Dash()
        {
            On();
            //a dash is a second
            Thread.Sleep(1000);
            Off();
        }

        public void Dot()
        {
            On();
            //a dot is a quarter second 
            Thread.Sleep(250);
            Off();
       }

        public void On()
        {
            led.Write(true);
        }
        
        public void Off()
        {
            led.Write(false);
        }
    }
}
