//
//  MyMainClass.h
//  BeBrave
//
//  Created by eran broder on 21/11/2018.
//  Copyright © 2018 Big Nerd Ranch. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM(NSInteger, Gesture) {
    none = 0,
    middleTap = 1,
    indexTap = 2,
    thumbTap = 3,
    twist = 4,
    doubleIndexTap = 5,
    doubleMiddleTap = 6,
    swipeLeft = 7,
    swipeRight = 8,
    longPress = 9,
    fitting = 10,
    pinching = 11,
    count
};

typedef NS_ENUM(NSInteger, CoreTestType) {
    QuaternionTest = 0
};

typedef NS_ENUM(NSInteger, PresureCalibrationMode) {
    notInCalibration = 0,
    min = 1,
    max = 2
};

typedef NS_ENUM(NSInteger, WindowQuality) {
    Good = 0,
    TooLong = 1,
    TooWeek = 2,
    TooStrog = 3,
    Bad = 4
};

typedef NS_ENUM(NSInteger, Hand) {
    Left =0,
    Right = 1
};


typedef NS_ENUM(NSInteger, AlgorithemMode) {
    AirMouseMode = 0,
    BasicWithoutQuaternion = 1
};


typedef NS_ENUM(NSInteger, FirmwareDataType) {
    Snc,
    
    //only inspire
    Imu,
    
    //Only mudraBand
    ImuNorm, ImuQuaternion, ImuGyro, ImuAccRaw
};

typedef NS_ENUM(NSInteger, CalibrationMode) {
    DefaultClassifier =0,
    Calibration = 1
};

typedef NS_ENUM(NSInteger, LoggingSeverity) {
    Debug = 0,
    Info = 1,
    Warning = 2,
    Error = 3
};

typedef NS_ENUM(NSInteger, License) {
    Main = 0,
    RawData = 1,
    TensorFlowData = 2,
    DoubleTap = 3,
};

typedef NS_ENUM(NSInteger, ConnectionState) {
    Disconnected = 0,
    Connecting = 1,
    Connected = 2,
    Disconnecting = 3,
    None = 4
};

typedef NS_ENUM(NSInteger, CallHandlingAction) {
    StartListening = 0,
    StopListening = 1,
    RecieveCall = 2,
    DismissCall = 3,
    CallHandlingActionCount
};

typedef NS_ENUM(NSInteger, MusicHandlingAction) {
    OpenMusicHandling = 0,
    CloseMusicHandling = 1,
    Play = 2,
    Pause = 3,
    VolumeUp = 4,
    VolumeDown = 5,
    Next = 6,
    Back = 7,
    MusicHandlingActionCount
};

typedef void (^__nullable OnProportionalBlock)(float value);
typedef void (^__nullable OnGestureBlock)(Gesture gesture);
typedef void (^__nullable OnRawBlock)(NSNumber *timestamp, NSArray<NSNumber *>* values);
typedef void (^__nullable OnCalibrationFinishedBlock)(NSString *jSonString);
typedef void (^__nullable OnCalibrationChangedBlock)(Gesture gesture, WindowQuality quality);
typedef void (^__nullable OnTensorFlowDataRecievedBlock)(NSArray<NSNumber *>* data);
typedef void (^__nullable OnLoggingMsgBlock)(NSString *jSonString);
typedef void (^__nullable OnAirMouseButtonChangedBlock)(unsigned char value);

@interface ComputationWrapper : NSObject

+(void) setLicense:(License)feature : (NSString*) license;
+(void) setCoreLoggingSeverity:(LoggingSeverity) severity;
+(void) setOnLoggingMessage:(OnLoggingMsgBlock)block;
+(LoggingSeverity) getLoggingSeverity;

+(float)getGestureConfidenceThresholdUp ;
+(void)setGestureConfidenceThresholdUp:(float) threshold;
+(float)getGestureConfidenceThresholdDown ;
+(void)setGestureConfidenceThresholdDown:(float) threshold;
+(float)getAirMouseThresholdUp ;
+(void)setAirMouseThresholdUp:(float) threshold;
+(float)getAirMouseThresholdDown;
+(void)setAirMouseThresholdDown:(float) threshold;
+(float)getPressureSmoothing ;
+(void)setPressureSmoothing:(float) pressureSmoothing;

-(id)init:(int)index;

-(void)handleReceivedData:(NSData*)data;



-(void)setOnProportionalReady:(OnProportionalBlock)block;
-(void)setOnGestureReady:(OnGestureBlock)block;
-(void)setOnSncReady:(OnRawBlock)block;
-(void)setOnImuQuaternionReady:(OnRawBlock)block;
-(void)setImuAccNormReady:(OnRawBlock)block;
-(void)setImuAccRawReady:(OnRawBlock)block;
-(void)setImuGyroReady:(OnRawBlock)block;
-(void)setOnCalibrationFinished:(OnCalibrationFinishedBlock)block;
-(void)setOnCalibrationChanged:(OnCalibrationChangedBlock)block;
-(void)setOnTensorFlowDataReady:(OnTensorFlowDataRecievedBlock)block;
-(void)setOnAirMouseButtonChanged:(OnAirMouseButtonChangedBlock)block;

// Calibration
-(void)calibrateGesture:(Gesture) gesture;
-(void)clear;
-(void)clearCurrentGestureCalibration;
-(void)refineGestureCalibration;
-(float)getCalibrationProgress;
-(void)clearGestureCalibration:(Gesture) gesture;
-(void)deleteLastGestureEmbedding;
-(void)setCalibration:(NSString*)jsonString;
-(void)setHand:(Hand) hand;
-(void)calibratePressure:(PresureCalibrationMode) presureCalibrationMode;
-(bool)isGestureCalibrationReady;
-(bool)isGestureCalibrationEnabled;
-(void)setIsGestureCalibrationEnabled:(bool) enabled;
-(void)SetAlgorithemMode:(AlgorithemMode) mode;
-(bool)isPressureCalibrationEnabled;
-(void)setIsPressureCalibrationEnabled:(bool) enabled;
-(void)setIsSwitchingSNCSensorsNeeded:(bool) enabled;
-(NSString*)GetModelVersion;
-(void)SetToCoolDown;
-(bool)isDataNeeded:(FirmwareDataType) dataType;

// Recording
-(bool)clearRecording;
-(void)enableRecording;
-(void)disableRecording;
-(void)stopRecording;
-(bool)startRecording: (NSString*) user: (NSString*) description;
-(void)deleteLastRecording;
-(NSString*)GetJsonRecording;
-(NSString*)GetCsvRecording;

@end

NS_ASSUME_NONNULL_END
