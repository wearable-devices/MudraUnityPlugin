#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mudra.Unity;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using System;

namespace Mudra.Unity
{
    public class
        MudraUnityiOSPlugin : PluginPlatform
    {


        public delegate void TestCallback();

        public delegate void onConnectedCallback(DeviceIdentifier[] data);
        public delegate void OnConnectedCallbackType(DeviceIdentifier[] data);


        public delegate void OnGestureCallback(GestureType gesture, DeviceIdentifier device);

        public delegate void OnGestureCallbackType(int gesture, DeviceIdentifier device);


        public delegate void OnPressureCallback(float gesture, DeviceIdentifier device);

        public delegate void OnPressureCallbackType(int gesture, DeviceIdentifier device);


        public delegate void OnQuaternionCallback(float x, float y, float z, float w, DeviceIdentifier device);

        public delegate void OnQuaternionCallbackType(float x, float y, float z, float w, DeviceIdentifier device);

        public delegate void OnMouseMovedCallbackType(float x, float y);
        public delegate void OnMouseMovedCallback(float x, float y);

        public delegate void OnImuAccRawCallback(float x, float y, float z, DeviceIdentifier device);

        public delegate void OnImuAccRawCallbackType(float x, float y, float z, DeviceIdentifier device);

        public delegate void OnSNCRawCallback(float x, float y, float z, DeviceIdentifier device);

        public delegate void OnSNCRawCallbackType(float x, float y, float z, DeviceIdentifier device);

        public delegate void OnMessageCallback(IntPtr data, DeviceIdentifier device);

        public delegate void OnMessageCallbackType(IntPtr data, DeviceIdentifier device);

        public static string test = "not init";

        public static bool createDevice;

        [MonoPInvokeCallback(typeof(OnConnectedCallbackType))]
        public static void OnConnected([MarshalAs(UnmanagedType.LPArray, SizeConst = 10)] DeviceIdentifier[] data)
        {

            DeviceIdentifier[] devicesData = GetDevicesExtern();
            devices.Clear();
            test = data.Length.ToString();

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].id == -1 || data[i].id > 10) continue;

                //MudraDevice newDevice = (MudraDevice)InputSystem.AddDevice(new InputDeviceDescription
                //{
                //    interfaceName = "Mudra",
                //    product = "Sample Mudra"
                //});
                //devices.Add(newDevice);
                deviceCreationQueue.Add(data[i]);
              //  SetOnGestureExtern(ref data[i], OnGesture);
               // SetOnQuaternionExtern(ref data[i], OnQuaternion);
                //SetOnPressureExtern(ref data[i], OnPressure);
                //SetOnImuAccRawExtern(ref data[i], OnImuAccRaw);
                //SetOnSNCRawExtern(ref devicesData[i], OnSNCRaw);
                //SetMessageRecievedExtern(ref devicesData[i], OnMessageRecieved);


            }
        }
        public override void SetupDevice(MudraDevice device)
        {
            Debug.Log("------Setting Up Device-------");
            SetOnGestureExtern(ref device.identifier, OnGesture);
            SetOnQuaternionExtern(ref device.identifier, OnQuaternion);
            SetOnPressureExtern(ref device.identifier, OnPressure);
       
            SetMessageRecievedExtern(ref device.identifier, OnMessageRecieved);
            SetOnMouseMovedExtern(OnMouseMoved);

        }

        public static MudraDevice CreateNewDevice()
        {
            MudraDevice newDevice = (MudraDevice)InputSystem.AddDevice(new InputDeviceDescription
            {
                interfaceName = "Mudra",
                product = "Sample Mudra"
            });

            return newDevice;
        }

        [DllImport("__Internal")]
        [return: MarshalAs(UnmanagedType.LPArray, SizeConst = 10)]
        private static extern void SetOnConnected(onConnectedCallback callback);

        [DllImport("__Internal")]
        private static extern void InitializePlugin();

        [DllImport("__Internal")]
        [return: MarshalAs(UnmanagedType.LPArray, SizeConst = 10)]
        private static extern DeviceIdentifier[] GetDevicesExtern();

        [DllImport("__Internal")]
        public static extern void SetOnGestureExtern    (ref DeviceIdentifier data, OnGestureCallback callback);

        [DllImport("__Internal")]
        public static extern void SetOnPressureExtern   (ref DeviceIdentifier data, OnPressureCallback callback);

        [DllImport("__Internal")]
        public static extern void SetOnQuaternionExtern (ref DeviceIdentifier data, OnQuaternionCallback callback);

        [DllImport("__Internal")]
        public static extern void SetOnMouseMovedExtern (OnMouseMovedCallback callback);

        [DllImport("__Internal")]
        public static extern void SetOnImuAccRawExtern(ref DeviceIdentifier data, OnImuAccRawCallback callback);

        [DllImport("__Internal")]
        public static extern void SetOnSNCRawExtern(ref DeviceIdentifier data, OnSNCRawCallback callback);

        [DllImport("__Internal")]
        public static extern void SetMessageRecievedExtern(ref DeviceIdentifier data, OnMessageCallback callback);

        [DllImport("__Internal")]
        public static extern void CleanUpMessageArray();

        [DllImport("__Internal")]
        public static extern void ResetQuatExtern(ref DeviceIdentifier data, float x, float y);

        [DllImport("__Internal")]
        public static extern void ConnectToDevicesExtern();

        [DllImport("__Internal")]
        public static extern void SetAirMouseModeExtern(ref DeviceIdentifier data, bool state);

        [DllImport("__Internal")]
        public static extern void SetAirMouseSpeedExtern(ref DeviceIdentifier data, int x, int y);

        [DllImport("__Internal")]
        public static extern void SetHandExtern(ref DeviceIdentifier data, int Hand);
        
        [DllImport("__Internal")]
        public static extern void SendFirmwareCommandExtern(ref DeviceIdentifier data,byte[] command,int size);
       

       

        [MonoPInvokeCallback(typeof(OnGestureCallbackType))]
        public static void OnGesture(GestureType gesture, DeviceIdentifier device)
        {

            devices[device.id].OnGesture(gesture);

        }

        [MonoPInvokeCallback(typeof(OnQuaternionCallback))]
        public static void OnQuaternion(float x, float y, float z, float w, DeviceIdentifier device)
        {

            devices[device.id].OnQuaternion(new Quaternion(x, y, z, w));

        }

        [MonoPInvokeCallback(typeof(OnPressureCallbackType))]
        public static void OnPressure(float pressure, DeviceIdentifier device)
        {
            devices[device.id].OnPressure(pressure);
        }

        [MonoPInvokeCallback(typeof(OnMouseMovedCallbackType))]
        public static void OnMouseMoved(float x,float y)
        {
            Debug.Log("Called");
            mousePos.x += x;
            mousePos.y += y;
            mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
            mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

        }

      
        [MonoPInvokeCallback(typeof(OnMessageCallbackType))]
        public static void OnMessageRecieved(IntPtr data, DeviceIdentifier device)
        {
            Byte[] dataAsBytes = new Byte[5];
            Marshal.Copy(data, dataAsBytes, 0, 5);
            CleanUpMessageArray();

            for (int i = 0; i < dataAsBytes.Length; i++)
            {
                Debug.Log("UnityVal: " + dataAsBytes[i]);
            }

            float[] sncData = {dataAsBytes[1], dataAsBytes[2], dataAsBytes[3] };
           // devices[device.id].OnSNCRaw(sncData);
            
        }

        public override void Close()
        {
            throw new System.NotImplementedException();
        }

        public override void Init(string calibrationFile = "")
        {
            InitializePlugin();
            SetOnConnected(OnConnected);
            SetOnMouseMovedExtern(OnMouseMoved);
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

     

        public override void UpdateOnFingerTipPressureCallback(int index)
        {
            if (devices[index].IsFingerTipPressureEnabled)
            {

                SetOnPressureExtern(ref devices[index].identifier, OnPressure);

            }
            else
            {

                SetOnPressureExtern(ref devices[index].identifier, null);

            }
        }

        public override void UpdateOnGestureReadyCallback(int index)
        {

            if (devices[index].IsGestureEnabled)
            {

                SetOnGestureExtern(ref devices[index].identifier, OnGesture);

            }
            else
            {

                SetOnGestureExtern(ref devices[index].identifier, null);

            }


        }

        public override void UpdateOnQuaternionReadyCallback(int index)
        {
            if (devices[index].IsImuQuaternionEnabled)
            {
                SetOnQuaternionExtern(ref devices[index].identifier, OnQuaternion);
            }
            else
            {
                SetOnQuaternionExtern(ref devices[index].identifier, null);

            }
        }

        public override void ResetQuaternion(int index)
        {
            ResetQuatExtern(ref devices[index].identifier, Screen.width, Screen.height);
        }

        public override void SwitchToDpad()
        {
            throw new System.NotImplementedException();
        }

        public override void SwitchToAirmouse(bool state)
        {
            for (int i = 0; i < devices.Count; i++)
            {
                SetAirMouseModeExtern(ref devices[i].identifier, state);
            }
          
        }

        //public override void SetAirMouseSpeed(int speed)
        //{
        //    for (int i = 0; i < devices.Count; i++)
        //    {
        //        SetAirMouseSpeedExtern(ref devices[i].identifier, speed,speed);
        //    }
        //}

        //public override void SetMainHand(int hand)
        //{
        //    for (int i = 0; i < devices.Count; i++)
        //    {
        //        SetHandExtern(ref devices[i].identifier, hand);
        //    }
        //}

        //public override void ChangeScale(int Scale)
        //{
        //    for (int i = 0; i < devices.Count; i++)
        //    {
               
        //        // Call unmanaged code
        //        switch (Scale)
        //        {
        //            case 0:
        //                SendFirmwareCommandInternal(ref devices[i].identifier, MudraConstants.PRESSURE_SCALE_LOW);
        //                break;
        //            case 1:
        //                SendFirmwareCommandInternal(ref devices[i].identifier, MudraConstants.PRESSURE_SCALE_MID);
        //                break;
        //            case 2:
        //                SendFirmwareCommandInternal(ref devices[i].identifier, MudraConstants.PRESSURE_SCALE_HIGH);

        //                break;


        //        }
        //    }
        //}
        public void SendFirmwareCommandInternal(ref DeviceIdentifier identifier, byte[] command)
        {
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(sizeof(byte) * command.Length);
            Marshal.Copy(command, 0, unmanagedPointer, command.Length);

            SendFirmwareCommandExtern(ref identifier, command, command.Length);

            Marshal.FreeHGlobal(unmanagedPointer);

        }

        //public override void SetPressureSensitivity(int sens)
        //{
        //    for (int i = 0; i < devices.Count; i++)
        //    {
        //        switch (sens)
        //        {
        //            case 0:
        //                SendFirmwareCommandInternal(ref devices[i].identifier, MudraConstants.PRESSURE_SENS_LOW);
        //                break;
        //            case 1:
        //                SendFirmwareCommandInternal(ref devices[i].identifier, MudraConstants.PRESSURE_SENS_MIDLOW);
        //                break;
        //            case 2:
        //                SendFirmwareCommandInternal(ref devices[i].identifier, MudraConstants.PRESSURE_SENS_MID);
        //                break;
        //            case 3:
        //                SendFirmwareCommandInternal(ref devices[i].identifier, MudraConstants.PRESSURE_SENS_MIDHIGH);
        //                break;
        //            case 4:
        //                SendFirmwareCommandInternal(ref devices[i].identifier, MudraConstants.PRESSURE_SENS_HIGH);
        //                break;
        //        }
        //    }
        //}
       

        public override void ClearQueues()
        {
            throw new System.NotImplementedException();
        }

        public override string getFirmwareVersion(int id)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateOnSNCReady(int index)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateOnImuRawCallback(int index)
        {
            throw new NotImplementedException();
        }

        public override void SendFirmwareCommand(byte[] command)
        {
            for (int i = 0; i < devices.Count; i++)
            {
                SendFirmwareCommandExtern(ref devices[i].identifier, command, command.Length);

            }
        }
    }
}
#endif