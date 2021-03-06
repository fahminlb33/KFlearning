using KFlearning.Core.Extensions;
using KFlearning.Core.Native;
using Microsoft.Win32;

namespace KFlearning.Core.Services
{
    public interface ISystemTweaker
    {
        string WallpaperPath { get; set; }
        bool LockWallpaper { get; set; }
        bool LockDesktop { get; set; }
        bool LockUsbCopying { get; set; }
        bool LockRegistryEditor { get; set; }
        bool LockTaskManager { get; set; }
        bool LockControlPanel { get; set; }

        void Query();
        void Apply();
    }

    public class SystemSettingsService : ISystemTweaker
    {
        public string WallpaperPath { get; set; }
        public bool LockWallpaper { get; set; }
        public bool LockDesktop { get; set; }
        public bool LockUsbCopying { get; set; }
        public bool LockRegistryEditor { get; set; }
        public bool LockTaskManager { get; set; }
        public bool LockControlPanel { get; set; }
        
        public void Query()
        {
            using (var systemKey = Registry.CurrentUser.OpenSubKey(NativeConstants.SystemPoliciesKey))
            using (var desktopKey = Registry.CurrentUser.OpenSubKey(NativeConstants.DesktopKey))
            using (var explorerKey = Registry.CurrentUser.OpenSubKey(NativeConstants.ExplorerKey))
            using (var storageKey = Registry.LocalMachine.OpenSubKey(NativeConstants.StoragePoliciesKey))
            using (var activeDesktopKey = Registry.CurrentUser.OpenSubKey(NativeConstants.ActiveDesktopKey))
            {
                LockWallpaper = activeDesktopKey.GetIntValue(NativeConstants.NoChangingWallPaper) == 1;
                WallpaperPath = systemKey.GetStringValue(NativeConstants.Wallpaper) ?? desktopKey.GetStringValue(NativeConstants.Wallpaper);
                LockDesktop = systemKey.GetIntValue(NativeConstants.NoDispCPL) == 1;
                LockRegistryEditor = systemKey.GetIntValue(NativeConstants.DisableRegistryTools) == 1;
                LockTaskManager = systemKey.GetIntValue(NativeConstants.DisableTaskMgr) == 1;
                LockUsbCopying = storageKey.GetIntValue(NativeConstants.WriteProtect) == 1;
                LockControlPanel = explorerKey.GetIntValue(NativeConstants.NoControlPanel) == 1;
            }
        }

        public void Apply()
        {
            using (var systemKey = Registry.CurrentUser.CreateSubKey(NativeConstants.SystemPoliciesKey))
            using (var desktopKey = Registry.CurrentUser.CreateSubKey(NativeConstants.DesktopKey))
            using (var explorerKey = Registry.CurrentUser.CreateSubKey(NativeConstants.ExplorerKey))
            using (var storageKey = Registry.LocalMachine.CreateSubKey(NativeConstants.StoragePoliciesKey))
            using (var activeDesktopKey = Registry.CurrentUser.CreateSubKey(NativeConstants.ActiveDesktopKey))
            {
                activeDesktopKey?.SetValue(NativeConstants.NoChangingWallPaper, LockWallpaper ? 1 : 0,
                    RegistryValueKind.DWord);
                desktopKey?.SetValue(NativeConstants.Wallpaper, WallpaperPath, RegistryValueKind.String);
                systemKey?.SetValue(NativeConstants.Wallpaper, WallpaperPath, RegistryValueKind.String);
                systemKey?.SetValue(NativeConstants.WallpaperStyle, 0, RegistryValueKind.DWord);
                systemKey?.SetValue(NativeConstants.NoDispCPL, LockDesktop ? 1 : 0, RegistryValueKind.DWord);
                systemKey?.SetValue(NativeConstants.DisableRegistryTools, LockRegistryEditor ? 1 : 0,
                    RegistryValueKind.DWord);
                systemKey?.SetValue(NativeConstants.DisableTaskMgr, LockTaskManager ? 1 : 0, RegistryValueKind.DWord);
                storageKey?.SetValue(NativeConstants.WriteProtect, LockUsbCopying ? 1 : 0, RegistryValueKind.DWord);
                explorerKey?.SetValue(NativeConstants.NoControlPanel, LockControlPanel ? 1 : 0,
                    RegistryValueKind.DWord);
            }
        }
    }
}