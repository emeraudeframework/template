using System.Reflection;
using Emeraude.Application.Mapping;

namespace EmStartTemplate.Admin.Mapping;

public class EmStartTemplateAdminAssemblyMappingProfile : AssemblyMappingProfile
{
    public EmStartTemplateAdminAssemblyMappingProfile()
        : base(Assembly.GetExecutingAssembly())
    {
    }
}