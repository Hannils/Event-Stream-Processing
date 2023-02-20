using System;
using ClassLibrary.Types;

namespace ClassLibrary.Classifier
{
    public class AnomalyClassifier : EventClassifier
    {
        public AnomalyClassifier()
        {
            this.Subscriptions = new string[] { "play", "user", "bookmark", "load" };
        }

        public string[] Subscriptions { get; }

        public void Classify(Event evt)
        {

        }
    }
}

