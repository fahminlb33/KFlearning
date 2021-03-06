namespace KFlearning.Core.Native
{
    internal static class NativeConstants
    {
        public static uint STANDARD_RIGHTS_READ = 0x00020000;
        public static uint TOKEN_QUERY = 0x0008;
        public static uint TOKEN_READ = STANDARD_RIGHTS_READ | TOKEN_QUERY;

        public const string UacRegistryKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
        public const string UacRegistryValue = "EnableLUA";

        public const string SystemPoliciesKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
        public const string ActiveDesktopKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\ActiveDesktop";
        public const string ExplorerKey = @"Software\Microsoft\Windows\Current Version\Policies\Explorer";
        public const string StoragePoliciesKey = @"SYSTEM\Current Control Set\Control\StorageDevicePolicies";
        public const string DesktopKey = @"Control Panel\Desktop";

        public const string NoChangingWallPaper = "NoChangingWallPaper";
        public const string Wallpaper = "Wallpaper";
        public const string WallpaperStyle = "WallpaperStyle";
        public const string NoDispCPL = "NoDispCPL";
        public const string DisableRegistryTools = "DisableRegistryTools";
        public const string DisableTaskMgr = "DisableTaskMgr";
        public const string WriteProtect = "WriteProtect";
        public const string NoControlPanel = "NoControlPanel";
    }
}