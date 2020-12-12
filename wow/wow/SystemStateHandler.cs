using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wow
{
    class SystemStateHandler : ISubject
    {
        public enum state_transtition_t{IDLE_TO_ACTIVE, ACTIVE_TO_IDLE};
        private List<IObserver> _observers = new List<IObserver>();
        private state_transtition_t stateTranstition;
        public state_transtition_t StateTranstition
        {
            get { return stateTranstition; }
        }

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

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                case SessionSwitchReason.SessionLogoff:
                    stateTranstition = state_transtition_t.ACTIVE_TO_IDLE;
                    break;
                case SessionSwitchReason.SessionLogon:
                case SessionSwitchReason.SessionUnlock:
                    stateTranstition = state_transtition_t.IDLE_TO_ACTIVE;
                    break;
                default:
                    break;
            }
            Notify();
        }
    }
}
