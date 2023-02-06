using System;
using ESPV1.Types;
namespace ESPV1.Classifier {
    
    public interface EventClassifier {
        public string[] Subscriptions { get; }
        public void Classify(Event evt);
    }
}

