namespace WJLThoughts.HardwareDevice.Camera
{
    /// <summary>
    /// 海康线阵相机
    /// </summary>
    public class MyCamera_HIKLinear : AbstractMyCamera_HIK
    {
        public override CameraTypes CameraType => CameraTypes.Linear;
        public MyCamera_HIKLinear(CameraConnectTypes connectType, uint cameraIndex) : base(connectType, cameraIndex)
        {

        }
        public MyCamera_HIKLinear(CameraConnectTypes connectType, string SerialNum) : base(connectType, SerialNum)
        {

        }
    }
}
