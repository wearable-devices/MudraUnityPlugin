

using System.Runtime.InteropServices;
using UnityEngine;

namespace Mudra.Unity
{
    public struct DeviceData
    {

        public Vector2? airMousePosDelta;
        public float? fingerTipPressure;
        public GestureType? lastGesture;
        public Quaternion? quaternion;
        public float? click;
        public bool sendToApp;
        public bool sendToHID;
    }

    [StructLayout(LayoutKind.Sequential,CharSet = CharSet.Ansi)]
    public struct DeviceIdentifier
    {
        public int id;
    }
}