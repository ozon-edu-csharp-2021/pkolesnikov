﻿using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.
    AggregationModels.ValueObjects
{
    public class EmployeeId : ValueObject
    {
        public long Value { get; }
        
        public EmployeeId(long id)
        {
            Value = id;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}