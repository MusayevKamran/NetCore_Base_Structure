namespace KIT.Redis.Models;

/// <summary>
///     Redis cache data model
/// </summary>
/// <typeparam name="TData">Type of cache data</typeparam>
public class RedisCacheDataModel<TData>
{
    public RedisCacheDataModel(TData data)
    {
        Data = data;
    }

    /// <summary>
    ///     Cache data
    /// </summary>
    public TData Data { get; set; }
}