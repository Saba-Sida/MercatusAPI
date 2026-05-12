namespace Application.BlobStorage;

/// <summary>
/// Generic Blob storage manager
/// </summary>
/// <remarks>
/// This file storage manager performs the storage operations safely,
/// meaning in case of any error and unsuccessful storing or file retrieval operation,
/// program won't crash, rather it will calmly return default value in every method where logical
/// </remarks>
public interface IGenericBlobStorageManager
{
    /// <summary>
    /// Saves file to given address
    /// </summary>
    /// <param name="addressToSave">
    /// Directory full address, including the directory name in the end
    /// </param>
    /// <param name="fullFileName">Complete file name</param>
    /// <remarks>
    /// If such address does not exist, creates.
    /// If such file already exists, overwrites
    /// </remarks>
    Task<string?> SaveFile(string addressToSave, string fullFileName, byte[]? content);
}