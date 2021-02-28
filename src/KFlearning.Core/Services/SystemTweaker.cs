using KFlearning.Core.Extensions;
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

    public class SystemTweaker : ISystemTweaker
    {
        private const string SystemPoliciesKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
        private const string ActiveDesktopKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\ActiveDesktop";
        private const string ExplorerKey = @"Software\Microsoft\Windows\Current Version\Policies\Explorer";
        private const string StoragePoliciesKey = @"SYSTEM\Current Control Set\Control\StorageDevicePolicies";
        private const string DesktopKey = @"Control Panel\Desktop";

        private const string NoChangingWallPaper = "NoChangingWallPaper";
        private const string Wallpaper = "Wallpaper";
        private const string WallpaperStyle = "WallpaperStyle";
        private const string NoDispCPL = "NoDispCPL";
        private const string DisableRegistryTools = "DisableRegistryTools";
        private const string DisableTaskMgr = "DisableTaskMgr";
        private const string WriteProtect = "WriteProtect";
        private const string NoControlPanel = "NoControlPanel";

        public string WallpaperPath { get; set; }
        public bool LockWallpaper { get; set; }
        public bool LockDesktop { get; set; }
        public bool LockUsbCopying { get; set; }
        public bool LockRegistryEditor { get; set; }
        public bool LockTaskManager { get; set; }
        public bool LockControlPanel { get; set; }

        public void Query()
        {
            using (var systemKey = Registry.CurrentUser.OpenSubKey(SystemPoliciesKey))
            using (var desktopKey = Registry.CurrentUser.OpenSubKey(DesktopKey))
            using (var explorerKey = Registry.CurrentUser.OpenSubKey(ExplorerKey))
            using (var storageKey = Registry.LocalMachine.OpenSubKey(StoragePoliciesKey))
            using (var activeDesktopKey = Registry.CurrentUser.OpenSubKey(ActiveDesktopKey))
            {
                LockWallpaper = activeDesktopKey.GetIntValue(NoChangingWallPaper) == 1;
                WallpaperPath = systemKey.GetStringValue(Wallpaper) ?? desktopKey.GetStringValue(Wallpaper);
                LockDesktop = systemKey.GetIntValue(NoDispCPL) == 1;
                LockRegistryEditor = systemKey.GetIntValue(DisableRegistryTools) == 1;
                LockTaskManager = systemKey.GetIntValue(DisableTaskMgr) == 1;
                LockUsbCopying = storageKey.GetIntValue(WriteProtect) == 1;
                LockControlPanel = explorerKey.GetIntValue(NoControlPanel) == 1;
            }
        }

        public void Apply()
        {
            using (var systemKey = Registry.CurrentUser.CreateSubKey(SystemPoliciesKey))
            using (var desktopKey = Registry.CurrentUser.CreateSubKey(DesktopKey))
            using (var explorerKey = Registry.CurrentUser.CreateSubKey(ExplorerKey))
            using (var storageKey = Registry.LocalMachine.CreateSubKey(StoragePoliciesKey))
            using (var activeDesktopKey = Registry.CurrentUser.CreateSubKey(ActiveDesktopKey))
            {
                activeDesktopKey?.SetValue(NoChangingWallPaper, LockWallpaper ? 1 : 0, RegistryValueKind.DWord);
                desktopKey?.SetValue(Wallpaper, WallpaperPath, RegistryValueKind.String);
                systemKey?.SetValue(Wallpaper, WallpaperPath, RegistryValueKind.String);
                systemKey?.SetValue(WallpaperStyle, 0, RegistryValueKind.DWord);
                systemKey?.SetValue(NoDispCPL, LockDesktop ? 1 : 0, RegistryValueKind.DWord);
                systemKey?.SetValue(DisableRegistryTools, LockRegistryEditor ? 1 : 0, RegistryValueKind.DWord);
                systemKey?.SetValue(DisableTaskMgr, LockTaskManager ? 1 : 0, RegistryValueKind.DWord);
                storageKey?.SetValue(WriteProtect, LockUsbCopying ? 1 : 0, RegistryValueKind.DWord);
                explorerKey?.SetValue(NoControlPanel, LockControlPanel ? 1 : 0, RegistryValueKind.DWord);
            }
        }
    }
}