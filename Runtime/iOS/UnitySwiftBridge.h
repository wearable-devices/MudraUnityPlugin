
#import <Mudra/Mudra-Swift.h>

#pragma mark - C interface

struct DeviceData{
//    char* name;
//    char* uuid;
    int id;
};

typedef void (*OnDeviceConnected)(DeviceData* data);
typedef void (*OnGesture)(Gesture gesture,DeviceData data);
typedef void (*OnPressure)(float pressure,DeviceData data);
typedef void (*OnQuaternion)(float x,float y,float z,float w,DeviceData data);
typedef void (*OnMouseMoved)(float x,float y);
typedef void (*OnImuAccRaw)(float x,float y,float z, DeviceData data);
typedef void (*OnSNCRaw)(float x,float y,float z, DeviceData data);
typedef void (*OnMessageRecieved)(char* message, DeviceData data);

const size_t MAX_DEVICES = 10;
OnDeviceConnected m_onMudraDeviceConnected;
OnMouseMoved m_onMouseMoved;

DeviceData* devicesData;

@interface MudraUnity : NSObject<MudraDelegate>
- (void) onMudraDeviceConnected:(MudraDevice *) device;
- (void) onDeviceConnectedByIos:(MudraDevice *) device;

- (void) onBluetoothStateChanged:(BOOL)state;
@end


@interface Test : UIViewController
//-(void) getMousePosition: (UIHoverGestureRecognizer*) hoverer;
@property BOOL prefersPointerLocked;
-(void) initialize;

@end

Test* test;


extern "C" {


void SetOnConnected(OnDeviceConnected callback);

void InitializePlugin();

void SetOnGestureExtern(DeviceData &data,OnGesture callback);

void SetOnPressureExtern(DeviceData &data,OnPressure callback);

void SetOnQuaternionExtern(DeviceData &data,OnQuaternion callback);

void SetOnImuAccRawExtern(DeviceData &data,OnImuAccRaw callback);

void SetOnSNCRawExtern(DeviceData &data,OnSNCRaw callback);

void SetOnMouseMovedExtern(OnMouseMoved calllback);

void ResetQuatExtern(DeviceData &data,float x,float y);

void SetMessageRecievedExtern(DeviceData &data,OnMessageRecieved callback);

void ConnectToDevicesExtern();
void CleanUpMessageArray();

const DeviceData* const GetDevicesExtern();

void SetAirMouseModeExtern( DeviceData &data, bool state);
void SetAirMouseSpeedExtern(DeviceData &data, int x, int y);
void SetHandExtern(DeviceData &data, int Hand);
void SendFirmwareCommandExtern(DeviceData &data,uint8_t* command,int size);
}


