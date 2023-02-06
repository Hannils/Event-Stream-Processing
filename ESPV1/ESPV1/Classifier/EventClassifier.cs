using System;
using ESPV1.Types;
namespace ESPV1.Classifier {
    
    public interface EventClassifier {
        public static String[] Subscriptions = new String[1];
        public void Classify(Event evt);
    }
}

