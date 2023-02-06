using System;
using ESPV1.Types;
namespace ESPV1.Classifier {
    public class AnomalyClassifier : EventClassifier {
        public AnomalyClassifier() {
            this.Subscriptions = new string[] { "POST_/play", "GET_/user", "POST_/bookmark" };
        }

        public string[] Subscriptions { get; }

        public void Classify(Event evt) {

        }
    }
}

