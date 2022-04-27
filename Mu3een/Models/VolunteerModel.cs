using Mu3een.Entities;

namespace Mu3een.Models
{
    public class VolunteerModel
    {
        public VolunteerModel()
        {

        } 
        public VolunteerModel(Volunteer model)
        {
            Id = model.Id;
            Name = model.Name;
            ImageUrl = model.ImageUrl;
            Gender = model.Gender;
            Age = model.Age;
            Points = model.Points;
        }
        public  Guid Id { get;  }
        public string? Name { get;  }
        public string? ImageUrl { get;  }
        public Gender? Gender { get;  }
        public int? Age { get;  }
        public int Points { get;  } = 0;
    }
}
