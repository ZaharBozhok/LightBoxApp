using System;
using System.Threading.Tasks;

namespace LightBoxApp.Services.Storage
{
    public interface IStorageService
    {
        Task<T> LoadAsync<T>(string key);
        Task SaveAsync<T>(string key, T val);
    }
}
