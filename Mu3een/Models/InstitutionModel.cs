﻿using Mu3een.Entities;

namespace Mu3een.Models
{
    public class InstitutionModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
