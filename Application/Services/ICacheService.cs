namespace Application.Services;

/// <summary>
/// Cache Memory Service, for fast value retrieval for temporary state values
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Creates or overwrites a value to cache memory
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="timeToLive"></param>
    /// <typeparam name="T"></typeparam>
    Task SetValueByKey<T>(string key, T value, TimeSpan timeToLive);
    
    /// <summary>
    /// Gets the value from cache
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Object of generic type T, Null if the key does not exist</returns>
    Task<T?> GetValueByKey<T>(string key);
}