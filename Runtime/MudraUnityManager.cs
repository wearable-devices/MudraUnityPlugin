#if MUDRA_ENABLED
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mudra.Unity;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using System;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class MudraUnityManager : MonoBehaviour
{

    public static PluginPlatform plugin;

    [SerializeField] float mousespeed;
    [SerializeField] int AirMouseSpeed = 5;
    [SerializeField] HandType Hand = HandType.Left;

    [SerializeField] bool AirMouseState = false;
    [SerializeField] bool Pressure = false;
    [SerializeField] bool Gesture = false;
    [SerializeField] bool Quaternion = false;

    [SerializeField] UnityEvent<int> OnConnectedEvent;
    [SerializeField] UnityEvent<int> OnDisConnectedEvent;




    public void SetNavigationState(bool state, int index)
    {
        PluginPlatform.devices[index].isNavigationEnabled = state;
    }
    public void SetPressureState(bool state, int index)
    {
        PluginPlatform.devices[index].IsFingerTipPressureEnabled = state;
    }
    public void SetGestureState(bool state, int index)
    {
        PluginPlatform.devices[index].IsGestureEnabled = state;
    }
    public void SetQuaternionState(bool state, int index)
    {
        PluginPlatform.devices[index].IsImuQuaternionEnabled = state;
    }

    public void SetAirMouseSpeed(int speed, int index)
    {
        if (plugin == null) return;

        plugin.SetNavigationSpeed(speed, index);
        AirMouseSpeed = speed;
    }
    public void SetHand(int hand, int index)
    {
        if (plugin == null) return;

        plugin.SetMainHand(hand, index);
        Hand = (HandType)hand;
    }
    public void SetFirmwareTarget(FirmwareTarget firmwareTarget, bool active, int index)
    {
        if (firmwareTarget == FirmwareTarget.NAVIGATION_TO_HID)
            PluginPlatform.devices[index].deviceData.sendToHID = active;


        if (firmwareTarget == FirmwareTarget.NAVIGATION_TO_APP)
            PluginPlatform.devices[index].deviceData.sendToApp = active;

        plugin.setFirmwareTarget(firmwareTarget,active,index);
    }

    public void SendFirmwareCommand(byte[] command)
    {
        plugin.SendFirmwareCommand(command);
    }


    public void ResetGesture()
    {
        PluginPlatform.devices[0].deviceData.lastGesture = GestureType.None;
    }


#if UNITY_EDITOR
    [MenuItem("Mudra/Create Mudra Manager")]
    public static void AddManager()
    {
        GameObject obj = new GameObject("MudraManager");
        obj.AddComponent<MudraUnityManager>();

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
#endif
    private void Start()
    {
        Init();

    }
    private void Update()
    {
        if (plugin != null)
        {

        }

#if ENABLE_INPUT_SYSTEM
        if (PluginPlatform.deviceCreationQueue.Count > 0)
        {

            for (int i = 0; i < PluginPlatform.deviceCreationQueue.Count; i++)
            {
                Debug.Log(PluginPlatform.deviceCreationQueue.Count);

                Debug.Log("Creating Device");
                DeviceIdentifier identifier = PluginPlatform.deviceCreationQueue[i];

                MudraDevice newDevice = (MudraDevice)InputSystem.AddDevice(new InputDeviceDescription
                {
                    interfaceName = "Mudra",
                    product = "Sample Mudra"
                });
                newDevice.identifier = identifier;
                PluginPlatform.deviceCreationQueue.Clear();

                PluginPlatform.devices.Add(newDevice);
                plugin.SetupDevice(newDevice);
                OnDeviceConnected(identifier.id);

            }



        }
#endif
    }
    public void Init()
    {

        if (plugin == null)
        {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN) || NETFX_CORE || UNITY_WSA || UNITY_WSA_10_0
            //  _pluginPlatform = new WindowsPlugin();
#elif (UNITY_ANDROID)
Debug.Log("Create New Unity Plugin");
            plugin = new MudraUnityAndroidPlugin();

#elif (UNITY_IOS)
            //Logger.Print("MudraUnityPlugin new iOS_PlugIn()");
            plugin = new  MudraUnityiOSPlugin();
#endif
        }
        Debug.Log("Init");
        if (plugin != null)
            plugin.Init();

    }
    public void OnDeviceConnected(int i)
    {
        PluginPlatform.devices[i].isNavigationEnabled = AirMouseState;
        PluginPlatform.devices[i].IsFingerTipPressureEnabled = Pressure;
        PluginPlatform.devices[i].IsGestureEnabled = Gesture;
        PluginPlatform.devices[i].IsImuQuaternionEnabled = Quaternion;

        SetFirmwareTarget(FirmwareTarget.NAVIGATION_TO_APP, false, i);
        SetFirmwareTarget(FirmwareTarget.NAVIGATION_TO_HID, false, i);

        SetAirMouseSpeed(AirMouseSpeed, i);
        SetHand((int)Hand, i);

        OnConnectedEvent.Invoke(i);
    }
    public void SetDeviceMode(DeviceMode mode,int index)
    {
        plugin.SetDeviceMode(mode, index);

    }
    public void SetDeviceMode(int mode)
    {
        plugin.SetDeviceMode((DeviceMode)mode,0);
    }
    public void OnDeviceDisconnected(int i)
    {
        OnDisConnectedEvent.Invoke(i);
    }
    public void GetDevices()
    {
#if UNITY_IOS

        MudraUnityiOSPlugin.ConnectToDevicesExtern();
#endif
    }

    private void OnApplicationQuit()
    {
        if (!PluginPlatform.HasDevices)
            return;
        SetPressureState(false, 0);
        SetGestureState(false, 0);
    }

}
#endif