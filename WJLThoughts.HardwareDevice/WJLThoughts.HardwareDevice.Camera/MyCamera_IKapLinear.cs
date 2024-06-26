﻿using System;

namespace WJLThoughts.HardwareDevice.Camera
{
    public class MyCamera_IKapLinear_BoardPCIE_New : AbstractMyCamera_IKap_BoardPCIE_New
    {
        public MyCamera_IKapLinear_BoardPCIE_New(uint cameraIndex, uint boardIndex, string configFilename, BoardInfoClasss boardInfoClasss= BoardInfoClasss.CameraLink) : base(cameraIndex, boardIndex, configFilename, boardInfoClasss)
        {
        }
        public MyCamera_IKapLinear_BoardPCIE_New(string serialNum, uint boardIndex, string configFilename, BoardInfoClasss boardInfoClasss = BoardInfoClasss.CameraLink) : base(serialNum, boardIndex,configFilename, boardInfoClasss)
        {
        }
        public override CameraTypes CameraType => CameraTypes.Linear;
        public override bool SaveImage(string savePath)
        {
            throw new NotImplementedException();
        }

        public override bool SoftWareTrigger()
        {
            throw new NotImplementedException();
        }
    }
    public class MyCamera_IKapLinear_Net : AbstractMyCamera_IKap_Net
    {
        public MyCamera_IKapLinear_Net(uint cameraIndex) : base(cameraIndex)
        {
        }
        public MyCamera_IKapLinear_Net(string serialNum) : base(serialNum)
        {
        }
        public override CameraTypes CameraType => CameraTypes.Linear;
        public override bool SaveImage(string savePath)
        {
            throw new NotImplementedException();
        }

        public override bool SoftWareTrigger()
        {
            throw new NotImplementedException();
        }
    }
}
