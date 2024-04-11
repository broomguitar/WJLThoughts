using System;

namespace WJLThoughts.HardwareDevice.Camera
{
    public class MyCamera_IkapArea_Net : AbstractMyCamera_IKap_Net
    {
        public MyCamera_IkapArea_Net(uint cameraIndex) : base(cameraIndex)
        {
        }
        public MyCamera_IkapArea_Net(string serialNum) : base(serialNum)
        {

        }
        public override CameraTypes CameraType => CameraTypes.Area;

        public override bool SoftWareTrigger()
        {
            throw new NotImplementedException();
        }
    }
}
