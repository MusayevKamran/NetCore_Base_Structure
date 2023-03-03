using AutoMapper;

namespace App.Application.Mapping;

/// <summary>
///     Mapping from Type
/// </summary>
/// <typeparam name="T">Type</typeparam>
public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}