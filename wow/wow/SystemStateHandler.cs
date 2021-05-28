using Microsoft.Win32;

namespace wow
{
    class SystemStateHandler : SubjectImplementation
    {
        public enum state_transtition_t{TO_ACTIVE, TO_INACTIVE};
        private state_transtition_t stateTranstition;
        public state_transtition_t StateTranstition
        {
            get { return stateTranstition; }
        }


        public SystemStateHandler() 
        { 
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            TopicBroker.publishTopic("SYSTEM_STATE_EVENT", this);
        }

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                case SessionSwitchReason.SessionLogoff:
                    new UserBreak().startBreak();
                    stateTranstition = state_transtition_t.TO_INACTIVE;
                    break;
                case SessionSwitchReason.SessionLogon:
                case SessionSwitchReason.SessionUnlock:
                    stateTranstition = state_transtition_t.TO_ACTIVE;
                    break;
                default:
                    break;
            }
            Notify();
        }
    }
}
