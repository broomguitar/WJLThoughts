using System;

namespace WJLThoughts.HardwareDevice.Camera
{
    public class HYCamera_IkapArea_Net : AbstractMyCamera_IKap_Net
    {
        public HYCamera_IkapArea_Net(uint cameraIndex) : base(cameraIndex)
        {
        }
        public HYCamera_IkapArea_Net(string serialNum) : base(serialNum)
        {

        }
        public override CameraTypes CameraType => CameraTypes.Area;

        public override bool SoftWareTrigger()
        {
            throw new NotImplementedException();
        }
    }
}
