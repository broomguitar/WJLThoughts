namespace WJLThoughts.HardwareDevice.Camera
{
    /// <summary>
    /// 海康面阵相机
    /// </summary>
    public class MyCamera_HIKArea : AbstractMyCamera_HIK
    {
        public override CameraTypes CameraType => CameraTypes.Area;
        public MyCamera_HIKArea(CameraConnectTypes connectType, uint cameraIndex) : base(connectType, cameraIndex)
        {

        }
        public MyCamera_HIKArea(CameraConnectTypes connectType, string SerialNum) : base(connectType, SerialNum)
        {

        }
    }
}
