using System;
using ESPV1.Types;
namespace ESPV1.Classifier {
    public class TrendingClassifier : EventClassifier {
        public TrendingClassifier() {
            this.Subscriptions = new string[] {"POST_/load"};
        }

        public string[] Subscriptions { get; }

        public void Classify(Event evt) {

        }
    }
}

