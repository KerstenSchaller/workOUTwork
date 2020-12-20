using Microsoft.Win32;

namespace wow
{
    class SystemStateHandler : SubjectImplementation
    {
        public enum state_transtition_t{IDLE_TO_ACTIVE, ACTIVE_TO_IDLE};
        private state_transtition_t stateTranstition;
        public state_transtition_t StateTranstition
        {
            get { return stateTranstition; }
        }


        public SystemStateHandler() 
        { 
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
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
