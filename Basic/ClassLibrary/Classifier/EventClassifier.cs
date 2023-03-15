using ClassLibrary.Types;
using ClassLibrary.Handler;

namespace ClassLibrary.Classifier
{
    public class EventClassifier
    {
        private Dictionary<string, List<IEventHandler>> SubscriptionMap;
        public EventClassifier(IEventHandler[] handlers)
        {
            SubscriptionMap = new Dictionary<string, List<IEventHandler>>();
            foreach (var handler in handlers) {
                foreach (var subscription in handler.Subscriptions) {
                    if (!SubscriptionMap.ContainsKey(subscription))
                        SubscriptionMap.Add(subscription, new List<IEventHandler>());
                    SubscriptionMap[subscription].Add(handler);
                }
            }
        }
        public IEventHandler[]? Classify(Event evt)
        {
            try {
                var list = SubscriptionMap[evt.identifier];
                return list.ToArray();
            } catch (KeyNotFoundException e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            return null;

        }
    }
}

