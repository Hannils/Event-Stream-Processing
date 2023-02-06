using System;
using ESPV1.Types;
using ESPV1.Classifier;
namespace ESPV1.Filter {
    public class EventFilter {
        private Dictionary<String, List<EventClassifier>> x;
        public EventFilter(EventClassifier[] classifiers) {
            x = new Dictionary<string, List<EventClassifier>>();
            foreach (var classifier in classifiers) {
                foreach (var subscription in classifier.Subscriptions) {
                    if (!x.ContainsKey(subscription)) x.Add(subscription, new List<EventClassifier>());
                    x[subscription].Add(classifier);
                }
            }
        }
        public EventClassifier[] filter(Event evt) {
            var list = x[evt.identifier];
            if (list == null) return null;
            return list.ToArray();
        }
    }
}

