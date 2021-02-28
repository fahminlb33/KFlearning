namespace KFlearning.Core.API
{
    public class DeviceIdentificationModel
    {
        public string DeviceId { get; set; }
        public double RAM { get; set; }
        public string CPU { get; set; }
        public string OS { get; set; }
        public string Architecture { get; set; }
    }
}
