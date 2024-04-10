namespace WJLThoughts.HardwareDevice.Camera
{
    public class MyCamera_DalsaLinear : AbstractMyCamera_Dalsa
    {
        public override CameraTypes CameraType => CameraTypes.Linear;
        public MyCamera_DalsaLinear(string serialNum, string configPath, int channel) : base(serialNum, configPath, channel)
        {

        }
        public MyCamera_DalsaLinear(string serialNum, int channel) : base(serialNum, channel)
        {

        }
    }
}
