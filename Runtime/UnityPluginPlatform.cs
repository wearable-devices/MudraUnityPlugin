#if MUDRA_ENABLED
using System.Collections.Generic;
using UnityEngine;
using Mudra.Unity;
using UnityEngine.Events;
namespace Mudra.Unity
{
    public delegate void OnLoggingMessageCallBack(string message);

    abstract public class PluginPlatform
    {
        public static string licenseEmail;
        public static List<DeviceIdentifier> deviceCreationQueue = new List<DeviceIdentifier>();
        public static List<MudraDevice> devices = new List<MudraDevice>();

        const int NUM_OF_SNCS = 3;

        abstract public void Init(string calibrationFile = "");
        abstract public void Update();
        abstract public void Close();
        abstract public void ResetQuaternion(int index);
        abstract public void SetMainHand(int hand, int index);
        abstract public void SetModelType(ModelType type, int index);
        abstract public void SetDeviceMode(DeviceMode deviceMode, int index);



        abstract public void SendFirmwareCommand(byte[] command);
        abstract public int GetBatteryLevel(int index);
        abstract public string GetFirmwareVersion(int id);
        abstract public long GetSerialNumber(int index);
        abstract public string GetDeviceNumber(int index);

        abstract protected string getLicenseForEmailFromCloud(string email);

        abstract public void setGestureActive(bool state, int index);
        abstract public void setPressureActive(bool state, int index);
        abstract public void setNavigationActive(bool state, int index);
        abstract public void SetQuaternionActive(bool state, int index);
        abstract public void setFirmwareTarget(FirmwareTarget firmwareTarget, bool active, int index);


        public delegate void onInitFunc();
        public event onInitFunc onInit;

        public UnityEvent<int> OnDeviceConnected = new UnityEvent<int>();
        public UnityEvent<int> OnDeviceDisconnected = new UnityEvent<int>();
        public UnityEvent<int> onDeviceConnectedByAndroidOS = new UnityEvent<int>();
        public UnityEvent<int> onDeviceFailedToConnect = new UnityEvent<int>();
        public UnityEvent<int> onDeviceConnecting = new UnityEvent<int>();
        public UnityEvent<int> onDeviceDisconnecting = new UnityEvent<int>();
        public static bool HasDevices
        {
            get => devices.Count > 0;
            private set => HasDevices = value;
        }
        public abstract void SetupDevice(MudraDevice device);
        abstract public void UpdateOnSNCReady(int index);

        public void Clear()
        {
            foreach (var device in devices)
            {
                device.deviceData.lastGesture = null;
                //device.deviceData.quaternion = null;

                device.deviceData.fingerTipPressure = null;
            }
        }
    }
}
#endif