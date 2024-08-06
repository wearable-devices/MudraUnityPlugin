
#include <Foundation/Foundation.h>
#include "UnitySwiftBridge.h"
#include <UIKit/UIKit.h>
#include <CoreGraphics/CGGeometry.h>
#include <GameController/GameController.h>
#include <iostream>
#pragma mark - C interface


GCMouse* mudraMouse;
GCKeyboard* mudraKeyboard;
float* temp =(float *) malloc(sizeof(float)*3);
char* thisChar;

int counter;
char* convertNSStringToCString(const NSString* nsString)
{
    if (nsString == NULL)
        return NULL;

    const char* nsStringUtf8 = [nsString UTF8String];
    //Unity knows how to translate null terminated char* into c# strings
    char* cString = (char*)malloc(strlen(nsStringUtf8) + 1);
    strcpy(cString, nsStringUtf8);

    return cString;
}


@implementation MudraUnity


- (void)onMudraDeviceConnected:(MudraDevice *)device
{

    
    DeviceData* data = (DeviceData*)malloc(sizeof(DeviceData)*MAX_DEVICES);
    
    
    printf("Started checking devices \n");
    
    NSArray<MudraDevice*>* devices = [[MudraManager shared] getDevices];
    
    for (size_t i = 0; i<MAX_DEVICES;i++){
        //NSLog(devices[i].name);
        //[devices[i] connect];
        if(i<devices.count){
            data[i] = {(int) i};
        }else{
            data[i] ={-1};
        }
        
    }
    
    printf("Device Count: %i \n",devices.count);
   
    // memcpy(&devices_saved,&data,sizeof(DeviceData)*MAX_DEVICES);
   // data[0] = {convertNSStringToCString(@"TestName"), convertNSStringToCString(@"TestUUID") };
    m_onMudraDeviceConnected(data);
}

-(void) onBluetoothStateChanged:(BOOL)state{
    printf("bluetooth state is now: %i  \n",state);
}

-(void) onDeviceConnectedByIos:(MudraDevice *)device{
    const char *s = [[device name] UTF8String];
    
    [device connect];
}
@end


MudraDevice* FindMudraDevice(const DeviceData& data){
    NSArray<MudraDevice*>* devices = [[MudraManager shared] getDevices];
    return  devices[data.id];
}


extern "C" {

//Set calllbacks
void SetOnConnected(OnDeviceConnected callback){
    
    m_onMudraDeviceConnected = callback;
    
}

void InitializePlugin(){
    id<MudraDelegate> delegate = [[MudraUnity alloc] init];
    [[MudraManager shared] setDelegate:(delegate)];
    
    test = [Test alloc];
    [test init];
    [test initialize];
    
    UIViewController* UnityView = UnityGetGLViewController();
    [UnityView addChildViewController:test];
    [UnityView childViewControllerForPointerLock];
    [UnityView setNeedsUpdateOfPrefersPointerLocked];
    
    
    mudraMouse = GCMouse.current;
    mudraKeyboard = GCKeyboard.coalescedKeyboard;
    //GCKeyboard.k
    
    [[MudraManager shared] initBluetouth];
    printf("Set On Connected \n");
    
    [MudraManager setLicense:Main :@"LicenseType::Main"];
    [MudraManager setLicense:RawData :@"LicenseType::RawData"];

}

void SetOnGestureExtern(DeviceData &data,OnGesture callback){
    MudraDevice* device = FindMudraDevice(data);
    printf("SetOnGesture");
    [device setOnGestureReady:^(Gesture gesture) {
        callback(gesture,data);
    }];
    
    printf("Gesture Callback Set on device %s \n",[device.name cStringUsingEncoding:NSUTF8StringEncoding]);
    
}


void SetOnPressureExtern(DeviceData &data,OnPressure callback){
    MudraDevice* device = FindMudraDevice(data);
    
    [device setOnProportionalReady:^(float pressure)
     {
        callback(pressure,data);
    }];
    
    printf("pressure Callback Set on device %s",[device.name cStringUsingEncoding:NSUTF8StringEncoding] );
    //[device sendFirmwareCommandWithCommmand:@[@(0x43),@(0x06)]];
}

void SetOnQuaternionExtern(DeviceData &deviceData,OnQuaternion callback){
    
    MudraDevice* device = FindMudraDevice(deviceData);
    
    printf("quaternion Callback Set on device %s \n",[device.name cStringUsingEncoding:NSUTF8StringEncoding]);
    
    [device setOnImuQuaternionReady:^(uint32_t time, NSArray<NSNumber *> * _Nonnull quat) {
       
        callback([quat[0] floatValue],[quat[1] floatValue],[quat[2] floatValue],[quat[3] floatValue],deviceData);
        
    }];
}


void SetOnMouseMovedExtern(OnMouseMoved calllback)
{
    m_onMouseMoved = calllback;
    
    mudraMouse.mouseInput.mouseMovedHandler = ^(GCMouseInput * _Nonnull mouseinp, float deltaX, float deltaY) {
        m_onMouseMoved(deltaX,deltaY);
        };
    
    mudraKeyboard.keyboardInput.keyChangedHandler = ^(GCKeyboardInput *keyboard, GCDeviceButtonInput *key, GCKeyCode keyCode, BOOL pressed){
        std::cout<<"---------------"<<key;
    };

    
}


void SetOnImuAccRawExtern(DeviceData &data,OnImuAccRaw callback)
{
    MudraDevice* device = FindMudraDevice(data);
    printf("ImuAccRaw Callback Set on device %s \n",[device.name cStringUsingEncoding:NSUTF8StringEncoding]);
   
    float* temp =(float *) malloc(sizeof(float)*3);
    //We get 8 samples of a vector3 for AccRaw so I sum them all and send the average
    [device setImuAccRawReady:^(uint32_t time, NSArray<NSNumber *> * value) {
       // printf("Test2");

        for (int i = 0; i<24; i+=3) {
            temp[0]+=[value[i] floatValue];
            temp[1]+=[value[i+1] floatValue];
            temp[2]+=[value[i+2] floatValue];

        }
        temp[0]/=24;
        temp[1]/=24;
        temp[2]/=24;
        
        callback(temp[0],temp[1],temp[2],data);
    }];
    
}
void SetOnSNCRawExtern(DeviceData &data,OnSNCRaw callback)
{
    
    if(counter>=200){
        temp[0]=0;
        temp[1]=0;
        temp[2]=0;
        counter=0;

    }
    
    MudraDevice* device = FindMudraDevice(data);
    printf("ImusncRaw Callback Set on device %s \n",[device.name cStringUsingEncoding:NSUTF8StringEncoding]);
   
    
    //We get 8 samples of a vector3 for AccRaw so I sum them all and send the average
    [device setOnSncPackageReady:^(uint32_t time, NSArray<NSNumber *> * value) {
        //printf("Test");
        
        for (int i = 0; i<18; i+=1)
        {
            temp[0]+= pow((value[i].floatValue),2);
            temp[1]+= pow(value[i+18].floatValue,2);
            temp[2]+= pow(value[i+(18*2)].floatValue,2);
        }
        
        counter+=18;
        if(counter>=200){
            
            temp[0]/=200;
            temp[1]/=200;
            temp[2]/=200;
            callback(sqrt(temp[2]),sqrt(temp[1]),sqrt(temp[0]),data);
        }
        //delete[] temp;
    }];
    
}

void ConnectToDevicesExtern(){
    NSArray<MudraDevice*>* devicesnotcon = [[MudraManager shared] getDevices];
    for (size_t i = 0; i<devicesnotcon.count;i++){
        
        NSLog(devicesnotcon[i].name);
        
        if(!devicesnotcon[i].isConnected){
            [devicesnotcon[i] connect];
            [devicesnotcon[i] setAlgorithemMode:BasicWithoutQuaternion ];
        }
        printf("isConnected: %i \n", [devicesnotcon[i] isConnected]);
    }
}

void ResetQuatExtern(DeviceData &data,float x,float y){
    MudraDevice* device = FindMudraDevice(data);
    float objects[] = {x,y};
    NSArray* arr =[NSArray arrayWithObjects:[NSNumber numberWithFloat:x],[NSNumber numberWithFloat:y],nil];
    
    [device resetAirMouseWithDimensions:(arr)];
}
const DeviceData* const GetDevicesExtern()
{
    
    DeviceData* data = (DeviceData*)malloc(sizeof(DeviceData)*MAX_DEVICES);
    
    
    printf("Started checking devices \n");
    
    NSArray<MudraDevice*>* devices = [[MudraManager shared] getDevices];
    
    for (size_t i = 0; i<devices.count;i++){
        NSLog(devices[i].name);
        //[devices[i] connect];
        data[i] = {(int) i};
    }
    
    printf("Device Count: %i \n",devices.count);
    
 
    return data;
}

void SetAirMouseModeExtern( DeviceData &data, bool state){
    MudraDevice* device = FindMudraDevice(data);
    NSArray<NSNumber*>* arr;
    if(state){
        
        arr = @[@(0x07),@(0x07),@(0x01)];
    }else{
        arr = @[@(0x07),@(0x07),@(0x00)];
    }
    [device sendFirmwareCommandWithCommmand:arr];
}

void SetAirMouseSpeedExtern(DeviceData &data, int x, int y){
    MudraDevice* device = FindMudraDevice(data);
    [device setAirMouseSpeedWithSpeed:@[@(x),@(y)]];
}
void SetHandExtern(DeviceData &data, int Hand){
    MudraDevice* device = FindMudraDevice(data);
    if(Hand==0){
       // [device setHandWithHand:Hand::Left];
    }
    else{
        //[device setHandWithHand:Hand::Right];
        
    }
}

void SendFirmwareCommandExtern(DeviceData &data,uint8_t* command,int size){
    NSMutableArray<NSNumber*>* arr = [NSMutableArray array];
    MudraDevice* device = FindMudraDevice(data);
    
    for (int i = 0; i<size; i++) {
        
        [arr addObject:[NSNumber numberWithInt:command[i]]];
        printf("%u",command[i]);
    }
    [device sendFirmwareCommandWithCommmand:arr];
}

void SetMessageRecievedExtern(DeviceData &data,OnMessageRecieved callback)
{
    MudraDevice* device = FindMudraDevice(data);
    printf("On Message Callback");
    [device setOnMessageReceived:^(NSData * _Nonnull message) {
        
        printf("MessageRecieved \n");
        
        thisChar = (char*)malloc(sizeof(char)*message.length);
       
        [message getBytes:thisChar length:message.length];
       
        if((Byte)thisChar[0]==0x99){
            printf("Recieved the right message");
            callback(thisChar,data);
        }
    }];
}


void CleanUpMessageArray(){
    delete [] thisChar;
    
}
}


@implementation Test
//UIHoverGestureRecognizer *hoverGestureRecognizer;
-(void) initialize{
    _prefersPointerLocked = true;

    
 
    //hoverGestureRecognizer.delegate = UnityView.view;
}

-(void) getMousePosition: (UIHoverGestureRecognizer*) recognizer{
   
    CGPoint point = [recognizer locationInView:nil];
    
    if(m_onMouseMoved !=nullptr)
        m_onMouseMoved(point.x,point.y);
}
@end





