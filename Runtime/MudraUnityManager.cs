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

    [SerializeField] string licenseEmail;
    [SerializeField] HandType Hand = HandType.Left;

    [SerializeField] bool navigationState = false;
    [SerializeField] bool pressureState = false;
    [SerializeField] bool gestureState = false;
    [SerializeField] bool quaternionState = false;

    [SerializeField] UnityEvent<int> OnConnectedEvent;
    [SerializeField] UnityEvent<int> OnDisconnectedEvent;
    [SerializeField] UnityEvent<int> OnDeviceConnectedByAndroidOSEvent = new UnityEvent<int>();
    [SerializeField] UnityEvent<int> OnDeviceFailedToConnectEvent = new UnityEvent<int>();
    [SerializeField] UnityEvent<int> OnDeviceConnectingEvent = new UnityEvent<int>();
    [SerializeField] UnityEvent<int> OnDeviceDisconnectingEvent = new UnityEvent<int>();



    public MudraDevice GetDevice(int index)
    {
        return PluginPlatform.devices[index];
    }
    public void SetNavigationState(bool state, int index)
    {
        plugin.setNavigationActive(state, index);
    }
    public void SetPressureState(bool state, int index)
    {
        plugin.setPressureActive(state, index);
    }
    public void SetGestureState(bool state, int index)
    {
        plugin.setGestureActive(state, index);
    }
    public void SetQuaternionState(bool state, int index)
    {
        plugin.SetQuaternionActive(state, index);
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

        plugin.setFirmwareTarget(firmwareTarget, active, index);
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
            PluginPlatform.licenseEmail = licenseEmail;
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
        {
            plugin.Init();

            plugin.OnDeviceDisconnected.AddListener(OnDeviceDisconnected);
            plugin.onDeviceConnectedByAndroidOS.AddListener(OnDeviceConnectedByAndroidOSEvent.Invoke);
            plugin.onDeviceConnecting.AddListener(OnDeviceConnectingEvent.Invoke);
            plugin.OnDeviceDisconnected.AddListener(OnDeviceDisconnectingEvent.Invoke);
            plugin.onDeviceFailedToConnect.AddListener(OnDeviceFailedToConnectEvent.Invoke);

        }
    }
    public void OnDeviceConnected(int i)
    {
        plugin.setNavigationActive(navigationState, i);
        plugin.setPressureActive(pressureState, i);
        plugin.setGestureActive(gestureState, i);
        SetQuaternionState(quaternionState, i);

        SetFirmwareTarget(FirmwareTarget.NAVIGATION_TO_APP, false, i);
        SetFirmwareTarget(FirmwareTarget.NAVIGATION_TO_HID, false, i);

        SetHand((int)Hand, i);

        OnConnectedEvent.Invoke(i);
    }
    public void SetDeviceMode(DeviceMode mode, int index)
    {
        plugin.SetDeviceMode(mode, index);

    }
    public void SetDeviceMode(int mode)
    {
        plugin.SetDeviceMode((DeviceMode)mode, 0);
    }
    public void OnDeviceDisconnected(int i)
    {
        OnDisconnectedEvent.Invoke(i);
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