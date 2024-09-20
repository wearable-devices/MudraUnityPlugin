
#if UNITY_ANDROID && MUDRA_ENABLED
using Mudra.Unity;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;



namespace Mudra.Unity
{
    public class MudraUnityAndroidPlugin : PluginPlatform
    {

        #region Android variables
        static AndroidJavaClass _mudraClass;
        static AndroidJavaObject _mudraDevices;
        static AndroidJavaObject _mudraInstance;

        static AndroidJavaObject devicesArrayJO;
        static List<AndroidJavaObject> devicesJO = new List<AndroidJavaObject>();

        string path = "Assets/Plugins/MudraSettings.asset";
        static Dictionary<AndroidJavaObject, int> javaObjectToIndexMap = new Dictionary<AndroidJavaObject, int>();

        public static string name = "not initialized";
        public bool _isConnected = false;

        #endregion
        public override void Init(string calibrationFile = "")
        {
            AndroidJNI.AttachCurrentThread();
            //find the main mudra class

            Debug.Log("Get Mudra Class");
            _mudraClass = new AndroidJavaClass("mudraAndroidSDK.model.Mudra");

            //Get application context
            AndroidJavaClass jcu = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jcu.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = jo.Call<AndroidJavaObject>("getApplicationContext");

            //Get mudra singleon instance
            _mudraInstance = _mudraClass.CallStatic<AndroidJavaObject>("getInstance");
            Debug.Log("Get Mudra Instance");


            //_mudraInstance.Call("setLicenseInternalUseOnly");
            Debug.Log("Setup License With Email:" + licenseEmail);
            _mudraInstance.Call("getLicenseForEmailFromCloud", licenseEmail, new OnGetEmailLicensesCallback());
            _mudraInstance.Call("requestAccessPermissions", jo);
            AndroidJavaObject topValue = new AndroidJavaClass("mudraAndroidSDK.enums.LoggingSeverity").GetStatic<AndroidJavaObject>("Info");

            _mudraInstance.Call("setCoreLoggingSeverity", topValue);
            Debug.Log("Set Mudra License");

            _mudraInstance.Call("setMudraDelegate", new MudraDelegate(this));

            // Get all mudra devices, we get an AndroidJavaObject which is a Java ArrayList
            devicesArrayJO = _mudraInstance.Call<AndroidJavaObject>("getBondedDevices", context);
            int size = devicesArrayJO.Call<int>("size");
            Debug.Log("Found:" + size + " devices");


            // we can then connect each device and create a new MudraDevice for each one
            for (int i = 0; i < size; i++)
            {
                AndroidJavaObject currDevice = devicesArrayJO.Call<AndroidJavaObject>("get", i);
                //devicesJO.Add(currDevice);
                currDevice.Call("connectDevice", context);
                //TODO: move to plugin platfrom under an add device function



            }

        }


        #region UpdateCallbacks


        #endregion


        #region Callback Interfaces
        class OnGestureReady : AndroidJavaProxy
        {
            int deviceId;
            public OnGestureReady(int id) : base("mudraAndroidSDK.interfaces.callback.OnGestureReady")
            {
                this.deviceId = id;
                Debug.Log("setGestureCallback");
            }

            void run(AndroidJavaObject retObj)
            {
                devices[deviceId].OnGesture((GestureType)retObj.Call<int>("ordinal"));

            }
        }
        class OnGetEmailLicensesCallback : AndroidJavaProxy
        {
            int deviceId;
            public OnGetEmailLicensesCallback() : base("mudraAndroidSDK.interfaces.callback.OnGetEmailLicensesCallback")
            {

            }

            void run(bool success, string result)
            {
                if (success)
                    Debug.Log("Result:" + result);
                else
                    Debug.Log("Failed!!!:" + result);

            }
        }
        class OnAirmouseButton : AndroidJavaProxy
        {
            int id;

            public OnAirmouseButton(int id) : base("mudraAndroidSDK.interfaces.callback.OnAirMouseButtonCallback")
            {
                this.id = id;
                Debug.Log("SetUpCallback");
            }

            void run(int button)
            {
                //  Debug.Log("Weee");

                // if (button == 0)
                // devices[id].onPress.Invoke();



                //  Debug.Log(quaternion);
            }
        }
        class OnFingertipPressureReady : AndroidJavaProxy
        {
            MudraUnityAndroidPlugin _unityPlugin;
            int id;

            public OnFingertipPressureReady(int id) : base("mudraAndroidSDK.interfaces.callback.OnPressureReady")
            {
                this.id = id;
                Debug.Log("setPressureCallback");
            }

            void run(float pressure)
            {
                devices[id].OnPressure(pressure /*/ 100.0f*/);


            }
        }
        class OnMessageReceived : AndroidJavaProxy
        {
            int id;
            public OnMessageReceived(int id) : base("mudraAndroidSDK.interfaces.callback.OnMessageReceived")
            {
                this.id = id;
                Debug.Log("Set Up On Message Received");
            }
            void run(byte[] data)
            {
                byte header = data[0];

                if (header == 0x60)
                {
                    //Pressure;
                    // Debug.Log("received pressure" + (float)data[1]);
                    //devices[id].deviceData.fingerTipPressure = (float)data[1] / 100.0f;
                }
                if (header == 0x61)
                {
                    //Pressure;
                    // Debug.Log("received gesture" + (NewGestureType)((int)data[1]));

                    devices[id].deviceData.lastGesture = (GestureType)((int)data[1]);
                }
            }

        }
        class OnImuQuaternionReady : AndroidJavaProxy
        {
            MudraUnityAndroidPlugin _unityPlugin;
            int id;

            public OnImuQuaternionReady(int id) : base("mudraAndroidSDK.interfaces.callback.OnImuQuaternionReady")
            {
                this.id = id;
            }

            void run(long ts, float[] q)
            {
                Quaternion quaternion = new Quaternion(q[0], q[1], q[2], q[3]);
                devices[id].OnQuaternion(quaternion);
            }
        }
        class OnNavigationReady : AndroidJavaProxy
        {
            int id;
            public OnNavigationReady(int id) : base("mudraAndroidSDK.interfaces.callback.OnNavigationReady")
            {
                this.id = id;
                Debug.Log("Set Up OnNavigationReady");
            }
            void run(int delta_x, int delta_y)
            {
                devices[id].OnMouseDelta(delta_x, delta_y);

            }
            void run(AndroidJavaObject direction)
            {
                Debug.Log(direction.Call<int>("ordinal"));
            }


        }
        class MudraDelegate : AndroidJavaProxy
        {
            MudraUnityAndroidPlugin plugin;
            public MudraDelegate(MudraUnityAndroidPlugin plugin) : base("mudraAndroidSDK.interfaces.MudraDelegate")
            {
                this.plugin = plugin;
                Debug.Log("Created Mudra Delegate");
            }


            void onDeviceDiscovered(AndroidJavaObject mudraDevice)
            {
                Debug.Log("Discovered Devices");
                AndroidJavaClass jcu = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jcu.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject context = jo.Call<AndroidJavaObject>("getApplicationContext");

                Debug.Log(mudraDevice.Get<string>("deviceName"));
                mudraDevice.Call("connectDevice", context);
            }

            void onDeviceConnected(AndroidJavaObject mudraDevice)
            {
                devicesJO.Add(mudraDevice);

                Debug.Log("Add Devices To Queue");
                Debug.Log(mudraDevice.Get<string>("deviceName"));

                DeviceIdentifier identifier;
                identifier.id = devices.Count;

                javaObjectToIndexMap.Add(mudraDevice, identifier.id);
                PluginPlatform.deviceCreationQueue.Add(identifier);
                plugin.OnDeviceConnected.Invoke(identifier.id);

            }

            void onDeviceDisconnected(AndroidJavaObject mudraDevice)
            {
                //Debug.Log("DeviceDisconnected");

                plugin.OnDeviceDisconnected.Invoke(devices.Count - 1);
                devices.RemoveAt(devices.Count - 1);
                devicesJO.RemoveAt(devicesJO.Count - 1);

            }
            void onDeviceConnectedByAndroidOS(AndroidJavaObject mudraDevice)
            {
               // plugin.onDeviceConnectedByAndroidOS.Invoke(javaObjectToIndexMap[mudraDevice]);
            }

            void onDeviceFailedToConnect(AndroidJavaObject mudraDevice)
            {
                plugin.onDeviceFailedToConnect.Invoke(javaObjectToIndexMap[mudraDevice]);

            }

            void onDeviceConnecting(AndroidJavaObject mudraDevice)
            {
                //plugin.onDeviceConnecting.Invoke(javaObjectToIndexMap[mudraDevice]);

            }

            void onDeviceDisconnecting(AndroidJavaObject mudraDevice)
            {
                plugin.onDeviceDisconnecting.Invoke(javaObjectToIndexMap[mudraDevice]);

            }

        }
        class OnButtonChanged : AndroidJavaProxy
        {
            int id;
            public OnButtonChanged(int id) : base("mudraAndroidSDK.interfaces.callback.OnButtonChanged")
            {
                this.id = id;
                Debug.Log("Set Up OnButtonChanged");
            }
            void run(AndroidJavaObject value)
            {
                devices[id].OnButtonChanged((NavigationButtons)value.Call<int>("ordinal"));
            }

        }
        #endregion
        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }



        public override void SetupDevice(MudraDevice device)
        {

            SetModelType(ModelType.Embedded, device.identifier.id);
            SetDeviceMode(DeviceMode.ANDROID, device.identifier.id);
        }

        #region General Use Commands
        public override void SetMainHand(int hand, int index)
        {
            if (!HasDevices) return;

            AndroidJavaObject topValue;
            if (hand == 0)
            {
                topValue = new AndroidJavaClass("mudraAndroidSDK.enums.HandType").GetStatic<AndroidJavaObject>("LEFT");
            }
            else
            {
                topValue = new AndroidJavaClass("mudraAndroidSDK.enums.HandType").GetStatic<AndroidJavaObject>("RIGHT");
            }


            devicesJO[index].Call("setHand", topValue);


        }

        public override int GetBatteryLevel(int index)
        {
            if (!HasDevices) return 0;

            return devicesJO[index].Call<int>("getBatteryLevel");
        }

        public override string GetFirmwareVersion(int id)
        {
            if (!HasDevices) return "...";

            return devicesJO[id].Call<string>("getFirmwareVersion");
        }

        public override long GetSerialNumber(int index)
        {
            if (!HasDevices || devices.Count<index) return 0;

            return devicesJO[index].Call<long>("getSerialNumber");
        }

        public override string GetDeviceNumber(int index)
        {
            if (!HasDevices || devices.Count < index) return "0";

            return devicesJO[index].Call<string>("getDeviceNumberByName");
        }

        public override void SendFirmwareCommand(byte[] command)
        {
            if (!HasDevices) return;

            for (int i = 0; i < devicesJO.Count; i++)
            {
                devicesJO[i].Call("sendGeneralCommand", command, null);
            }
        }
     
        public override void setGestureActive(bool state, int index)
        {
            if (!HasDevices || devices.Count < index) return;

            if (state)
                devicesJO[index].Call("setOnGestureReady", new OnGestureReady(index));

            else
                devicesJO[index].Call("disableGesture");
        }

        public override void setPressureActive(bool state, int index)
        {
            if (!HasDevices || devices.Count < index) return;


            if (state)
            {

                devicesJO[index].Call("setOnPressureReady", new OnFingertipPressureReady(index));

            }
            else
            {

                devicesJO[index].Call("disablePressure");
            }

        }

        public override void setNavigationActive(bool state, int index)
        {
            if (!HasDevices || devices.Count < index) return;

            if (state)
            {

                devicesJO[index].Call("setOnNavigationReady", new OnNavigationReady(index));
                // setNavigationActive(true);
                devicesJO[index].Call("setOnButtonChanged", new OnButtonChanged(index));



            }
            else
            {

                devicesJO[index].Call("disableNavigation");
                // setNavigationActive(false);
                devicesJO[index].Call("disableButtonChanged");


            }
        }

        public override void SetQuaternionActive(bool state, int index)
        {
            if (!HasDevices || devices.Count < index) return;

            if (state)
            {
                devicesJO[index].Call("setOnImuQuaternionReady", new OnImuQuaternionReady(index));
            }
            else
            {
                devicesJO[index].Call("disableQuaternion");
            }
        }

        public override void setFirmwareTarget(FirmwareTarget firmwareTarget, bool active, int index)
        {
            if (!HasDevices || devices.Count < index) return;

            AndroidJavaObject target = new AndroidJavaClass("mudraAndroidSDK.enums.FirmwareTarget").GetStatic<AndroidJavaObject>(firmwareTarget.ToString());

            devicesJO[index].Call("setFirmwareTarget", target, active);
        }
        public override void SetModelType(ModelType type, int index)
        {
            if (!HasDevices || devices.Count < index) return;

            AndroidJavaObject target = new AndroidJavaClass("mudraAndroidSDK.enums.ModelType").GetStatic<AndroidJavaObject>(type.ToString());

            devicesJO[index].Call<bool>("setModelType", target);
        }

        public override void SetDeviceMode(DeviceMode deviceMode, int index)
        {
            if (!HasDevices || devices.Count < index) return;

            AndroidJavaObject target = new AndroidJavaClass("mudraAndroidSDK.enums.DeviceMode").GetStatic<AndroidJavaObject>(deviceMode.ToString());

            devicesJO[index].Call("setDeviceMode", target);
        }
        #endregion

        #region NoLongerSupported



        class OnImuAccRawReady : AndroidJavaProxy
        {
            int id;
            public OnImuAccRawReady(int id) : base("mudraAndroidSDK.interfaces.callback.OnImuAccRawReady")
            {
                this.id = id;
            }

            void run(long timestamp, float[] data)
            {

                //Data is a flattened array of 8 samples at once, so the size of data is 24, in order to filter down any noise I take the averge of all samples
                float[] dataVector = new float[3];
                for (int i = 0; i < data.Length - 3; i += 3)
                {
                    dataVector[0] += data[i];
                    dataVector[1] += data[i + 1];
                    dataVector[2] += data[i + 2];


                }

                dataVector[0] /= data.Length;
                dataVector[1] /= data.Length;
                dataVector[2] /= data.Length;

                // devices[id].OnAccRaw(dataVector);

            }


        }
        class OnSNCReady : AndroidJavaProxy
        {
            int id;

            public OnSNCReady(int id) : base("mudraAndroidSDK.interfaces.callback.OnSncReady")
            {
                this.id = id;
            }

            void run(long ts, float[] snc)
            {
                for (int i = 0; i < snc.Length; i++)
                {
                    Debug.Log(snc[i]);
                }
            }

        }
        public override void UpdateOnSNCReady(int index)
        {

            devicesJO[index].Call("setOnSncReady", new OnSNCReady(index));
            Debug.Log("sncSet");
        }

        public override void ResetQuaternion(int index)
        {
            int[] dimentions = { Screen.width, Screen.height };
            devicesJO[index].Call("resetAirMouse", dimentions);
        }

        protected override string getLicenseForEmailFromCloud(string email)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}



public enum LoggingSeverity
{
    Debug,
    Info,
    Warning,
    Error
}
#endif