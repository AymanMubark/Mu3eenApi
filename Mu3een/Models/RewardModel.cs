﻿using Mu3een.Entities;

namespace Mu3een.Models
{
    public class RewardModel
    {
        public RewardModel()
        {

        } 

        public RewardModel(Reward model)
        {
            Id = model.Id;
            Name = model.Name;  
            Content = model.Content; 
            ImageUrl = model.ImageUrl;
            Points = model.Points;
            Numbers = model.Numbers;
            InstitutionId = model.InstitutionId;
            CreatedAt = model.CreatedAt;
        }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public int? Points { get; set; }
        public int? Numbers { get; set; }
        public Guid? InstitutionId { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
