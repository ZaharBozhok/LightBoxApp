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
            {//84:F3:EB:5A:1F:8A // "84:F3:EB:5A:1F:8A"
                HashSet<DeviceModel> deviceModels = await _storageService.LoadAsync<HashSet<DeviceModel>>(Constants.DevicesKey);
                if (deviceModels == null)
                    deviceModels = new HashSet<DeviceModel>();
                if (string.IsNullOrEmpty(deviceModel.Name))
                    deviceModel.Name = Constants.DefaultLightBoxName;
                if (deviceModel.Panel == "0")
                    deviceModel.Panel = "1";
                var res = deviceModels.Where(x => x.Mac == deviceModel.Mac);
                if (!res.Any())
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
                    updating.IsEnabled = deviceModel.IsEnabled;
                    await AddDeviceAsync(updating);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("UpdateDeviceAsync");
                Debug.WriteLine(ex.Message);
            }

        }
        public async Task AddPresetAsync(PresetModel presetModel)
        {
            try
            {
                List<PresetModel> presetModels = await _storageService.LoadAsync<List<PresetModel>>(Constants.PresetsKey);
                if (presetModels == null)
                    presetModels = new List<PresetModel>();
                var res = presetModels.Where(x => x.id == presetModel.id);
                if (!res.Any())
                {
                    presetModels.Add(presetModel);
                    await _storageService.SaveAsync<List<PresetModel>>(Constants.PresetsKey, presetModels);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddPresetAsync");
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<List<PresetModel>> GetPresetsAsync()
        {
            List<PresetModel> presetModels = await _storageService.LoadAsync<List<PresetModel>>(Constants.PresetsKey);
            if (presetModels != null)
                return presetModels;
            return new List<PresetModel>();
        }

        public async Task RemovePresetAsync(Guid id)
        {
            List<PresetModel> presetModels = await _storageService.LoadAsync<List<PresetModel>>(Constants.PresetsKey);
            if (presetModels != null)
            {
                presetModels.RemoveAll(x => x.id == id);
                await _storageService.SaveAsync<List<PresetModel>>(Constants.PresetsKey, presetModels);
            }
        }

        public async Task UpdatePresetAsync(PresetModel presetModel)
        {
            try
            {
                List<PresetModel> presetModels = await _storageService.LoadAsync<List<PresetModel>>(Constants.PresetsKey);
                if(presetModels == null || presetModels.Count == 0)
                {
                    await AddPresetAsync(presetModel);
                    return;
                }
                var updating = presetModels.Find(x => x.id == presetModel.id);
                if (updating != null)
                {
                    await RemovePresetAsync(updating.id);
                    updating.FirstPanel = presetModel.FirstPanel;
                    updating.SecondPanel = presetModel.SecondPanel;
                    updating.ThirdPanel = presetModel.ThirdPanel;
                    updating.Name = presetModel.Name;
                    await AddPresetAsync(updating);
                }
                else
                {
                    await AddPresetAsync(updating);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdatePresetAsync");
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task UpdatePresetByNameAsync(PresetModel presetModel)
        {
            try
            {
                List<PresetModel> presetModels = await _storageService.LoadAsync<List<PresetModel>>(Constants.PresetsKey);
                if(presetModels == null || presetModels.Count == 0)
                {
                    await AddPresetAsync(presetModel);
                    return;
                }
                var updating = presetModels.Find(x => x.Name == presetModel.Name);
                if (updating != null)
                {
                    await RemovePresetAsync(updating.id);
                    updating.FirstPanel = presetModel.FirstPanel;
                    updating.SecondPanel = presetModel.SecondPanel;
                    updating.ThirdPanel = presetModel.ThirdPanel;
                    await AddPresetAsync(updating);
                }
                else
                {
                    await AddPresetAsync(presetModel);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdatePresetByNameAsync");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
