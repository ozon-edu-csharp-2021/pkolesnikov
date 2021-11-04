﻿using System;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchPack : Entity
    {
        public MerchPackId MerchPackId { get; }
        
        public MerchPackType MerchPackType { get; }
        
        public InitiatingEventName? InitiatingEventName { get; private set; }

        public MerchPack(MerchPackId id, MerchPackType packType, InitiatingEventName? eventName)
            : this(id, packType)
        {
            SetInitiatingEventName(eventName);
        }
        
        public MerchPack(MerchPackId id, MerchPackType packType)
        {
            MerchPackId = id;
            MerchPackType = packType;
        }
        
        public void SetInitiatingEventName(InitiatingEventName? eventName)
        {
            if (eventName is null)
                throw new ArgumentNullException(nameof(eventName));

            if (MerchPackType.Equals(MerchPackType.ConferenceListener)
                || MerchPackType.Equals(MerchPackType.ConferenceListener)
                || MerchPackType.Equals(MerchPackType.ConferenceSpeaker))
                InitiatingEventName = eventName;
        }
    }
}