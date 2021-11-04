﻿using System;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.Exceptions.IssuanceRequestAggregate;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.IssuanceRequestAggregate
{
    public class IssuanceRequest : Entity
    {
        private RequestStatus _requestStatus = RequestStatus.InWork;
        
        public RequestNumber? RequestNumber { get; }

        public RequestStatus RequestStatus
        {
            get => _requestStatus;
            private set
            {
                _requestStatus = value;
                NewStatusDate = new NewStatusDate(DateTime.UtcNow);
            }
        }

        public NewStatusDate NewStatusDate { get; private set; } = new(DateTime.UtcNow);
        
        public MerchPackId MerchPackId { get; }
        
        public EmployeeId EmployeeId { get; }

        public IssuanceRequest(EmployeeId employeeId, MerchPackId merchPackId, RequestNumber? number = null)
        {
            EmployeeId = employeeId;
            MerchPackId = merchPackId;
            RequestNumber = number;
        }

        public void SetRequestStatus(RequestStatus newStatus)
        { 
            if (RequestStatus.Equals(RequestStatus.Done))
                throw new AlreadyDoneIssuanceRequest($"The application (id={RequestNumber.Value}) was completed");
            RequestStatus = newStatus; 
        }
    }
}