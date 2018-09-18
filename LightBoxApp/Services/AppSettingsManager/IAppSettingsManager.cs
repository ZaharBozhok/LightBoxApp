using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LightBoxApp.Models;

namespace LightBoxApp.Services.AppSettingsManager
{
    public interface IAppSettingsManager
    {
        Task<HashSet<DeviceModel>> GetDevicesAsync();
        Task AddDeviceAsync(DeviceModel deviceModel);
        Task RemoveDeviceAsync(DeviceModel deviceModel);
        Task UpdateDeviceAsync(DeviceModel deviceModel);

        Task<List<PresetModel>> GetPresetsAsync();
        Task AddPresetAsync(PresetModel presetModel);
        Task RemovePresetAsync(Guid id);
        Task UpdatePresetAsync(PresetModel presetModel);
        Task UpdatePresetByNameAsync(PresetModel presetModel);
    }
}
