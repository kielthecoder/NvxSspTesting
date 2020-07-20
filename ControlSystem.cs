using System;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro.DM.Streaming;

namespace NvxSspTesting
{
    public class ControlSystem : CrestronControlSystem
    {
        private DmNvx350 _tx;
        private DmNvx350 _rx;

        public ControlSystem()
            : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 25;
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        public override void InitializeSystem()
        {
            try
            {
                _tx = new DmNvx350(0x30, this);
                _tx.OnlineStatusChange += nvx_OnlineStatusChange;
                _tx.Register();

                _rx = new DmNvx350(0x31, this);
                _rx.OnlineStatusChange += nvx_OnlineStatusChange;
                _rx.Register();
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
                ErrorLog.Error(e.StackTrace);
            }
        }

        private void nvx_OnlineStatusChange(GenericBase dev, OnlineOfflineEventArgs args)
        {
            if (args.DeviceOnLine)
            {
                var nvxDevice = dev as DmNvx350;

                CrestronConsole.PrintLine("{0} is ONLINE", nvxDevice.Name);
            }
        }
    }
}