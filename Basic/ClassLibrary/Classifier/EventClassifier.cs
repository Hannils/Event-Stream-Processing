using System;
using ClassLibrary.Types;

namespace ClassLibrary.Classifier
{
    public interface EventClassifier
    {
        public string[] Subscriptions { get; }
        public void Classify(Event evt);
    }
}

