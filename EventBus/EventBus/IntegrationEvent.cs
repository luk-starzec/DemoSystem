﻿using System;

namespace EventBus
{
    public class IntegrationEvent
    {
        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }
    }
}
