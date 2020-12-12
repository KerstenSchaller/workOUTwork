using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wow
{
    class SystemStateHandler: ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public SystemStateHandler() 
        { 
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }

        public void Attach(IObserver observer)
        {
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        public int lock_count = 0;
        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                case SessionSwitchReason.SessionLogoff:
                    lock_count++;
                    Console.WriteLine("Session state to inactive");
                    break;
                case SessionSwitchReason.SessionUnlock:
                case SessionSwitchReason.SessionLogon:
                    Console.WriteLine("Session state to active");
                    break;
                
                default:
                    Console.WriteLine("Unhandled session state switch occured");
                    break;
            }
        }
    }
}
