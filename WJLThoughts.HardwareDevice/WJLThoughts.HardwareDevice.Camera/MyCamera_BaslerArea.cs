namespace WJLThoughts.HardwareDevice.Camera
{
    public class MyCamera_BaslerArea : AbstractMyCamera_Basler
    {
        public override CameraTypes CameraType => CameraTypes.Area;
        public MyCamera_BaslerArea(CameraConnectTypes connectType, uint cameraIndex) : base(connectType, cameraIndex)
        {

        }
        public MyCamera_BaslerArea(string serialNum) : base(serialNum)
        {

        }
    }
}
