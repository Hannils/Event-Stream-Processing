using System;
using ClassLibrary.Types;
using ClassLibrary.Classifier;
using System.Collections.Generic;

namespace ClassLibrary.Filter
{
    public class EventFilter
    {
        private Dictionary<string, List<EventClassifier>> x;
        public EventFilter(EventClassifier[] classifiers)
        {
            x = new Dictionary<string, List<EventClassifier>>();
            foreach (var classifier in classifiers) {
                foreach (var subscription in classifier.Subscriptions) {
                    if (!x.ContainsKey(subscription))
                        x.Add(subscription, new List<EventClassifier>());
                    x[subscription].Add(classifier);
                }
            }
        }
        public EventClassifier[]? Filter(Event evt)
        {
            try {
                var list = x[evt.identifier];
                return list.ToArray();
            } catch (KeyNotFoundException e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            return null;

        }
    }
}

