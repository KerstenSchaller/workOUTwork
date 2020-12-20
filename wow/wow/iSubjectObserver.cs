using System.Collections.Generic;

namespace wow
{
    public interface IObserver
    {
        // Receive update from subject
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        // Attach an observer to the subject.
        void Attach(IObserver observer);

        // Detach an observer from the subject.
        void Detach(IObserver observer);

        // Notify all observers about an event.
        void Notify();
    }

    public class SubjectImplementation : ISubject
    {

        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            this._observers.Add(observer);
            this.Notify();
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

    }



}

