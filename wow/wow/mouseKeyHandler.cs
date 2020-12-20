
using Gma.System.MouseKeyHook;

using System.Windows.Forms;

namespace wow
{
    class MouseKeyHandler : SubjectImplementation
    {
        public enum source_t { MOUSE_MOVED, KEY_PRESSED };
        private IKeyboardMouseEvents m_Events;
        private source_t source;

        public MouseKeyHandler()
        {
            this.Subscribe(Hook.GlobalEvents());
        }

        public source_t getSource() 
        {
            return this.source;
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyPress += HookManager_KeyPress;
            m_Events.MouseMove += HookManager_MouseMove;
        }


        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            this.source = source_t.MOUSE_MOVED;
            this.Notify();

        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.source = source_t.KEY_PRESSED;
            this.Notify();
        }


        private void Unsubscribe()
        {
            if (m_Events == null) return;
            m_Events.KeyPress -= HookManager_KeyPress;
            m_Events.MouseMove -= HookManager_MouseMove;
            m_Events.Dispose();
            m_Events = null;
        }
    }
}
