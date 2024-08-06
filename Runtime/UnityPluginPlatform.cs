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
        const int NUM_OF_SNCS = 3;

        public static List<MudraDevice> devices = new List<MudraDevice>();
        public static Vector2 mousePos = new Vector2(Screen.width / 2, Screen.height / 2);
        abstract public void Init(string calibrationFile = "");
        abstract public void Update();
        abstract public void Close();
        abstract public void ResetQuaternion(int index);
        abstract public void SwitchToDpad();
        abstract public void SwitchToAirmouse(bool state);
        abstract public void SetNavigationSpeed(int speed, int index);
        abstract public void SetMainHand(int hand, int index);
        abstract public void SetModelType(ModelType type, int index);
        abstract public void SetDeviceMode(DeviceMode deviceMode,int index );
        

       // abstract public void ChangeScale(int Scale,int index);
       // abstract public void SetPressureSensitivity(int sens, int index);
        abstract public void ClearQueues();
        abstract public void SendFirmwareCommand(byte[] command);


        //new navigation
      //  abstract public void setAirMousePointerActive(bool state);
      //  abstract public void setAirMousePressReleaseActive(bool state);
        abstract public void setGestureActive(bool state);
        abstract public void setPressureActive(bool state);
        abstract public void setAirTouchActive(bool state);
        abstract public void setFirmwareTarget(FirmwareTarget firmwareTarget, bool active,int index);
      //  abstract public void setNavigationActive(bool state);
        abstract public void UpdateNavigationCallback(int index);

        public virtual void MousePos() { }
        public static List<DeviceIdentifier> deviceCreationQueue = new List<DeviceIdentifier>();

        public delegate void onInitFunc();
        public event onInitFunc onInit;

        public UnityEvent OnDeviceConnected = new UnityEvent();
        public UnityEvent OnDeviceDisconnected = new UnityEvent();
        public static bool HasDevices
        {
            get => devices.Count > 0;
            private set => HasDevices = value;
        }

        #region OnGestureReady

        abstract public void UpdateOnGestureReadyCallback(int index);
        #endregion

        #region OnFingerTipPressureReady

        abstract public void UpdateOnFingerTipPressureCallback(int index);
        abstract public void UpdateOnImuRawCallback(int index);

        #endregion

        public abstract string getFirmwareVersion(int id);
        public abstract void SetupDevice(MudraDevice device);
        abstract public void UpdateOnQuaternionReadyCallback(int index);
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