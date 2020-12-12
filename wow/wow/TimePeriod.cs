using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wow
{
    class TimePeriod : IObserver
    {
        private MouseKeyHandler mouseKeyHandler = new MouseKeyHandler();
        private Int64 keyCount = 0;
        private Int64 mouseCount = 0;
        public Int64 KeyCount 
        {
            get { return keyCount; }
        }
        public Int64 MouseCount
        {
            get { return mouseCount; }
        }

        public TimePeriod() 
        {
            mouseKeyHandler.Attach(this);
        }

        public void Update(ISubject subject)
        {
            switch (mouseKeyHandler.getSource()) 
            {
                case MouseKeyHandler.source_t.KEY_PRESSED:
                    keyCount++;
                    break;
                case MouseKeyHandler.source_t.MOUSE_MOVED:
                    mouseCount++;
                    break;
            }
            
        }

    }
}
