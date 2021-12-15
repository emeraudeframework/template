using System.Reflection;
using Emeraude.Application.Mapping;

namespace EmStartTemplate.Application.Mapping;

public class EmStartTemplateAssemblyMappingProfile : AssemblyMappingProfile
{
    public EmStartTemplateAssemblyMappingProfile()
        : base(Assembly.GetExecutingAssembly())
    {
    }
}