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
        private IKeyboardMouseEvents m_Events;
        private List<IObserver> _observers = new List<IObserver>();

        public MouseKeyHandler() 
        {
            this.Subscribe(Hook.GlobalEvents());
        }

        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }


        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyPress += HookManager_KeyPress;
            m_Events.MouseMove += HookManager_MouseMove;



        }
        /*Mouse Handlers*/
        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mouse moved");
            this.Notify();

        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
             Console.WriteLine("Key pressed");
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
