namespace Mudra.Unity
{
    public enum HandType
    {
        Left,
        Right
    };

    public enum GestureType
    {
        None,
        MiddleTap,
        IndexTap,
        Thumb,
        BasicGesturesLength,
        Snap,
        Bloom,
        PinkyTap,
        ModelGesturesLength,
        Twist,
        DoubleIndexTap,
        DoubleMiddleTap,
        SwipeLeft,
        SwipeRight,
        LongPress,
        Fitting,
        Pinching,
        Tap,
        DoubleTwist
    }

    public enum NavigationButtons
    {

        Release,
        Press,
    }
    public enum FirmwareTarget
    {
        NAVIGATION_TO_APP,
        NAVIGATION_TO_HID,
        GESTURE_TO_HID
    };
    public enum Feature
    {
        RawData,
        TensorFlowData,
        DoubleTap
    };

    public enum LoggingSeverity
    {
        Debug,
        Info,
        Warning,
        Error
    };
    public enum MudraScale
    {
        LOW = 0,
        MID = 1,
        HIGH = 2
    }

    public enum MudraSensitivity
    {
        LOW = 0,
        MID_LOW = 1,
        MID = 2,
        MID_HIGH = 3,
        HIGH = 4
    }
    public enum ModelType
    {
        Basic,
        BasicWithoutQuaternion,
        NeuralClicker,
        Embedded
    }

    public enum DeviceMode
    {
        IPHONE,
        MAC,
        APPLE_TV,
        IPAD,
        ANDROID,
        CUSTOM
    }
}