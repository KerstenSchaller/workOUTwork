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
    class MouseKeyHandler
    {

        private IKeyboardMouseEvents m_Events;

        public void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyPress += HookManager_KeyPress;
            m_Events.MouseMove += HookManager_MouseMove;



        }
        /*Mouse Handlers*/
        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mouse moved");

        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
             Console.WriteLine("Key pressed");
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
