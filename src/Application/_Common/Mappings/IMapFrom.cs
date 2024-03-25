using AutoMapper;

namespace ChinookStore.Application._Common.Mappings;

public interface IMapFrom<T>
{   
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}