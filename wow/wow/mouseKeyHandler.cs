using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.Implementation;

using System.Windows.Forms;

namespace wow
{
    class MouseKeyHandler : ISubject
    {
        public enum source_t { MOUSE_MOVED, KEY_PRESSED };
        private IKeyboardMouseEvents m_Events;
        private List<IObserver> _observers = new List<IObserver>();
        private source_t source;

        public MouseKeyHandler() 
        {
            this.Subscribe(Hook.GlobalEvents());
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
