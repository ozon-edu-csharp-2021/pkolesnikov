﻿namespace OzonEdu.MerchandiseApi.Infrastructure.Repositories.Models
{
    public record Employee 
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public string? EmailAddress { get; init; }
        public string? ManagerEmailAddress { get; init; }
    }
}