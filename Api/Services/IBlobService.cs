using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IBlobService
    {
        Task<string> DownloadFileAsString(string fileName, string containerName);
        Task<List<T>> GetJsonListFromBlob<T>(string fileName, string containerName);
        Task WriteListAsJsonToBlob<T>(string fileName, string containerName, List<T> list);
    }
}