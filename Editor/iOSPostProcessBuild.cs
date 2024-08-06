#if UNITY_IOS && MUDRA_ENABLED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
public class iOSPostProcessBuild : MonoBehaviour
{
    [PostProcessBuild]
   public static void AddPlistValues(BuildTarget buildTarget, string pathToBuildProject)
    {
        //const string addKey = "Privacy - Bluetooth Always Usage Description";
        const string addKey = "NSBluetoothAlwaysUsageDescription";
        Debug.Log("Build targer:" + buildTarget);
        Debug.Log("BuildPath:" + pathToBuildProject);
      
        string plistPath = pathToBuildProject + "/Info.plist";
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);
        PlistElementDict root = plist.root;

       // root.CreateArray(addKey).AddString("Mudra Uses Bluetooth To Connect");
        root[addKey] = new PlistElementString("Mudra Uses Bluetooth To Connect");
        root.CreateArray("Required background modes").AddString("App communicates using CoreBluetooth");
        //root.SetString(addKey).
        plist.WriteToFile(plistPath);
    }
}
#endif