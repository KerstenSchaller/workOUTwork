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


    public static class TopicBroker 
    {
        private struct TopicObserver
        {
            public string topic;
            public IObserver observer;
        }

        private static SortedDictionary<string, ISubject> subjectMap = new SortedDictionary<string, ISubject>();
        private static List<TopicObserver> observerWaitList = new List<TopicObserver>();

        public static void publishTopic(string topic, ISubject subject) 
        {
            subjectMap.Add(topic, subject);

        //go through waitlist and see if theres an observer waiting for the topic
            foreach (TopicObserver topicObserver in observerWaitList) 
            {
                if( topicObserver.topic == topic) 
                {
                    subject.Attach(topicObserver.observer);
                }
            }
        }

        public static void subscribeTopic(string topic, IObserver observer) 
        {
            if (subjectMap.ContainsKey(topic))
            {
                subjectMap[topic].Attach(observer);
            }
            else
            {
                //topic not available, put on waitlist
                TopicObserver topicObserver;
                topicObserver.topic = topic;
                topicObserver.observer = observer;
                observerWaitList.Add(topicObserver);
            }
        }

    }



}

