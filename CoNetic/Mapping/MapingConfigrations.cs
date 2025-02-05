using CoNetic.Core.Models;
using CoNetic.DTOs;
using Mapster;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoNetic.Mapping
{
    public static class MapingConfigrations
    {
       
        public static void RegisterMappings()
        {
            TypeAdapterConfig<User, ProfileDTO>.NewConfig()
           .Map(dest => dest.Experiences, src => src.experiences.Adapt<List<ExperienceDTO>>())
           .Map(dest => dest.Skills, src => src.skills.Adapt<List<SkillDTO>>());

           TypeAdapterConfig<ProfileDTO, User>.NewConfig()
          .Map(dest => dest.experiences, src => src.Experiences.Adapt<List<Experience>>())
          .Map(dest => dest.skills, src => src.Skills.Adapt<List<Skill>>());
        }
        
    }
}
