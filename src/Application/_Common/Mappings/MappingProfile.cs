using System.Reflection;
using AutoMapper;

namespace ChinookStore.Application._Common.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile(Assembly assembly)
  {
    const string mappingMethodName = nameof(IMapFrom<object>.Mapping);
    var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();
    var argumentTypes = new[] { typeof(Profile) };

    foreach (var type in types)
    {
      var instance = Activator.CreateInstance(type);
      var methodInfo = type.GetMethod(mappingMethodName);

      if (methodInfo != null)
      {
        methodInfo.Invoke(instance, [this]);
      }
      else
      {
        var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

        if (interfaces.Count <= 0)
        {
          continue;
        }
        foreach (var interfaceMethodInfo in interfaces.Select(i => i.GetMethod(mappingMethodName, argumentTypes)))
        {
          interfaceMethodInfo?.Invoke(instance, [this]);
        }
      }
    }
  }

  private static bool HasInterface(Type t)
  {
    return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IMapFrom<>);
  }
}
