using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get; }

        private SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public static SubscriptionInfo Create(Type handlerType)
        {
            return new SubscriptionInfo(handlerType);
        }
    }
}
