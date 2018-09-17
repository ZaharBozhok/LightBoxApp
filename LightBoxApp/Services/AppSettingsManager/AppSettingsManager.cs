using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LightBoxApp.Models;
using LightBoxApp.Services.Storage;

namespace LightBoxApp.Services.AppSettingsManager
{
    public class AppSettingsManager : IAppSettingsManager
    {
        private readonly IStorageService _storageService;

        public AppSettingsManager(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task AddDeviceAsync(DeviceModel deviceModel)
        {
            try
            {
                HashSet<DeviceModel> deviceModels = await _storageService.LoadAsync<HashSet<DeviceModel>>(Constants.DevicesKey);
                if (deviceModels == null)
                    deviceModels = new HashSet<DeviceModel>();
                if (string.IsNullOrEmpty(deviceModel.Name))
                    deviceModel.Name = Constants.DefaultLightBoxName;
                if (deviceModel.Panel == "0")
                    deviceModel.Panel = "1";
                if (deviceModels.Count == 0 || deviceModels.First(x => x.Mac == deviceModel.Mac) == null)
                {
                    deviceModels.Add(deviceModel);
                    await _storageService.SaveAsync<HashSet<DeviceModel>>(Constants.DevicesKey, deviceModels);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("AddDeviceAsync");
                Debug.WriteLine(ex.Message);
            }

        }

        public async Task<HashSet<DeviceModel>> GetDevicesAsync()
        {
            HashSet<DeviceModel> deviceModels = await _storageService.LoadAsync<HashSet<DeviceModel>>(Constants.DevicesKey);
            if (deviceModels != null)
                return deviceModels;
            return new HashSet<DeviceModel>();
        }

        public async Task RemoveDeviceAsync(DeviceModel deviceModel)
        {
            HashSet<DeviceModel> deviceModels = await _storageService.LoadAsync<HashSet<DeviceModel>>(Constants.DevicesKey);
            if(deviceModels != null )
            {
                deviceModels.RemoveWhere(x => x.Mac == deviceModel.Mac);
                await _storageService.SaveAsync<HashSet<DeviceModel>>(Constants.DevicesKey, deviceModels);
            }
        }

        public async Task UpdateDeviceAsync(DeviceModel deviceModel)
        {
            try
            {
                HashSet<DeviceModel> deviceModels = await _storageService.LoadAsync<HashSet<DeviceModel>>(Constants.DevicesKey);
                var updating = deviceModels.First(x => x.Mac == deviceModel.Mac);
                if (updating != null)
                {
                    await RemoveDeviceAsync(updating);
                    updating.Name = deviceModel.Name;
                    updating.Panel = deviceModel.Panel;
                    await AddDeviceAsync(updating);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("UpdateDeviceAsync");
                Debug.WriteLine(ex.Message);
            }

        }
    }
}
