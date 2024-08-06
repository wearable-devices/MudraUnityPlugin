#if 0
#elif defined(__arm64__) && __arm64__
// Generated by Apple Swift version 5.9 (swiftlang-5.9.0.128.108 clang-1500.0.40.1)
#ifndef MUDRA_SWIFT_H
#define MUDRA_SWIFT_H
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wgcc-compat"

#if !defined(__has_include)
# define __has_include(x) 0
#endif
#if !defined(__has_attribute)
# define __has_attribute(x) 0
#endif
#if !defined(__has_feature)
# define __has_feature(x) 0
#endif
#if !defined(__has_warning)
# define __has_warning(x) 0
#endif

#if __has_include(<swift/objc-prologue.h>)
# include <swift/objc-prologue.h>
#endif

#pragma clang diagnostic ignored "-Wauto-import"
#if defined(__OBJC__)
#include <Foundation/Foundation.h>
#endif
#if defined(__cplusplus)
#include <cstdint>
#include <cstddef>
#include <cstdbool>
#include <cstring>
#include <stdlib.h>
#include <new>
#include <type_traits>
#else
#include <stdint.h>
#include <stddef.h>
#include <stdbool.h>
#include <string.h>
#endif
#if defined(__cplusplus)
#if defined(__arm64e__) && __has_include(<ptrauth.h>)
# include <ptrauth.h>
#else
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wreserved-macro-identifier"
# ifndef __ptrauth_swift_value_witness_function_pointer
#  define __ptrauth_swift_value_witness_function_pointer(x)
# endif
# ifndef __ptrauth_swift_class_method_pointer
#  define __ptrauth_swift_class_method_pointer(x)
# endif
#pragma clang diagnostic pop
#endif
#endif

#if !defined(SWIFT_TYPEDEFS)
# define SWIFT_TYPEDEFS 1
# if __has_include(<uchar.h>)
#  include <uchar.h>
# elif !defined(__cplusplus)
typedef uint_least16_t char16_t;
typedef uint_least32_t char32_t;
# endif
typedef float swift_float2  __attribute__((__ext_vector_type__(2)));
typedef float swift_float3  __attribute__((__ext_vector_type__(3)));
typedef float swift_float4  __attribute__((__ext_vector_type__(4)));
typedef double swift_double2  __attribute__((__ext_vector_type__(2)));
typedef double swift_double3  __attribute__((__ext_vector_type__(3)));
typedef double swift_double4  __attribute__((__ext_vector_type__(4)));
typedef int swift_int2  __attribute__((__ext_vector_type__(2)));
typedef int swift_int3  __attribute__((__ext_vector_type__(3)));
typedef int swift_int4  __attribute__((__ext_vector_type__(4)));
typedef unsigned int swift_uint2  __attribute__((__ext_vector_type__(2)));
typedef unsigned int swift_uint3  __attribute__((__ext_vector_type__(3)));
typedef unsigned int swift_uint4  __attribute__((__ext_vector_type__(4)));
#endif

#if !defined(SWIFT_PASTE)
# define SWIFT_PASTE_HELPER(x, y) x##y
# define SWIFT_PASTE(x, y) SWIFT_PASTE_HELPER(x, y)
#endif
#if !defined(SWIFT_METATYPE)
# define SWIFT_METATYPE(X) Class
#endif
#if !defined(SWIFT_CLASS_PROPERTY)
# if __has_feature(objc_class_property)
#  define SWIFT_CLASS_PROPERTY(...) __VA_ARGS__
# else
#  define SWIFT_CLASS_PROPERTY(...) 
# endif
#endif
#if !defined(SWIFT_RUNTIME_NAME)
# if __has_attribute(objc_runtime_name)
#  define SWIFT_RUNTIME_NAME(X) __attribute__((objc_runtime_name(X)))
# else
#  define SWIFT_RUNTIME_NAME(X) 
# endif
#endif
#if !defined(SWIFT_COMPILE_NAME)
# if __has_attribute(swift_name)
#  define SWIFT_COMPILE_NAME(X) __attribute__((swift_name(X)))
# else
#  define SWIFT_COMPILE_NAME(X) 
# endif
#endif
#if !defined(SWIFT_METHOD_FAMILY)
# if __has_attribute(objc_method_family)
#  define SWIFT_METHOD_FAMILY(X) __attribute__((objc_method_family(X)))
# else
#  define SWIFT_METHOD_FAMILY(X) 
# endif
#endif
#if !defined(SWIFT_NOESCAPE)
# if __has_attribute(noescape)
#  define SWIFT_NOESCAPE __attribute__((noescape))
# else
#  define SWIFT_NOESCAPE 
# endif
#endif
#if !defined(SWIFT_RELEASES_ARGUMENT)
# if __has_attribute(ns_consumed)
#  define SWIFT_RELEASES_ARGUMENT __attribute__((ns_consumed))
# else
#  define SWIFT_RELEASES_ARGUMENT 
# endif
#endif
#if !defined(SWIFT_WARN_UNUSED_RESULT)
# if __has_attribute(warn_unused_result)
#  define SWIFT_WARN_UNUSED_RESULT __attribute__((warn_unused_result))
# else
#  define SWIFT_WARN_UNUSED_RESULT 
# endif
#endif
#if !defined(SWIFT_NORETURN)
# if __has_attribute(noreturn)
#  define SWIFT_NORETURN __attribute__((noreturn))
# else
#  define SWIFT_NORETURN 
# endif
#endif
#if !defined(SWIFT_CLASS_EXTRA)
# define SWIFT_CLASS_EXTRA 
#endif
#if !defined(SWIFT_PROTOCOL_EXTRA)
# define SWIFT_PROTOCOL_EXTRA 
#endif
#if !defined(SWIFT_ENUM_EXTRA)
# define SWIFT_ENUM_EXTRA 
#endif
#if !defined(SWIFT_CLASS)
# if __has_attribute(objc_subclassing_restricted)
#  define SWIFT_CLASS(SWIFT_NAME) SWIFT_RUNTIME_NAME(SWIFT_NAME) __attribute__((objc_subclassing_restricted)) SWIFT_CLASS_EXTRA
#  define SWIFT_CLASS_NAMED(SWIFT_NAME) __attribute__((objc_subclassing_restricted)) SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_CLASS_EXTRA
# else
#  define SWIFT_CLASS(SWIFT_NAME) SWIFT_RUNTIME_NAME(SWIFT_NAME) SWIFT_CLASS_EXTRA
#  define SWIFT_CLASS_NAMED(SWIFT_NAME) SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_CLASS_EXTRA
# endif
#endif
#if !defined(SWIFT_RESILIENT_CLASS)
# if __has_attribute(objc_class_stub)
#  define SWIFT_RESILIENT_CLASS(SWIFT_NAME) SWIFT_CLASS(SWIFT_NAME) __attribute__((objc_class_stub))
#  define SWIFT_RESILIENT_CLASS_NAMED(SWIFT_NAME) __attribute__((objc_class_stub)) SWIFT_CLASS_NAMED(SWIFT_NAME)
# else
#  define SWIFT_RESILIENT_CLASS(SWIFT_NAME) SWIFT_CLASS(SWIFT_NAME)
#  define SWIFT_RESILIENT_CLASS_NAMED(SWIFT_NAME) SWIFT_CLASS_NAMED(SWIFT_NAME)
# endif
#endif
#if !defined(SWIFT_PROTOCOL)
# define SWIFT_PROTOCOL(SWIFT_NAME) SWIFT_RUNTIME_NAME(SWIFT_NAME) SWIFT_PROTOCOL_EXTRA
# define SWIFT_PROTOCOL_NAMED(SWIFT_NAME) SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_PROTOCOL_EXTRA
#endif
#if !defined(SWIFT_EXTENSION)
# define SWIFT_EXTENSION(M) SWIFT_PASTE(M##_Swift_, __LINE__)
#endif
#if !defined(OBJC_DESIGNATED_INITIALIZER)
# if __has_attribute(objc_designated_initializer)
#  define OBJC_DESIGNATED_INITIALIZER __attribute__((objc_designated_initializer))
# else
#  define OBJC_DESIGNATED_INITIALIZER 
# endif
#endif
#if !defined(SWIFT_ENUM_ATTR)
# if __has_attribute(enum_extensibility)
#  define SWIFT_ENUM_ATTR(_extensibility) __attribute__((enum_extensibility(_extensibility)))
# else
#  define SWIFT_ENUM_ATTR(_extensibility) 
# endif
#endif
#if !defined(SWIFT_ENUM)
# define SWIFT_ENUM(_type, _name, _extensibility) enum _name : _type _name; enum SWIFT_ENUM_ATTR(_extensibility) SWIFT_ENUM_EXTRA _name : _type
# if __has_feature(generalized_swift_name)
#  define SWIFT_ENUM_NAMED(_type, _name, SWIFT_NAME, _extensibility) enum _name : _type _name SWIFT_COMPILE_NAME(SWIFT_NAME); enum SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_ENUM_ATTR(_extensibility) SWIFT_ENUM_EXTRA _name : _type
# else
#  define SWIFT_ENUM_NAMED(_type, _name, SWIFT_NAME, _extensibility) SWIFT_ENUM(_type, _name, _extensibility)
# endif
#endif
#if !defined(SWIFT_UNAVAILABLE)
# define SWIFT_UNAVAILABLE __attribute__((unavailable))
#endif
#if !defined(SWIFT_UNAVAILABLE_MSG)
# define SWIFT_UNAVAILABLE_MSG(msg) __attribute__((unavailable(msg)))
#endif
#if !defined(SWIFT_AVAILABILITY)
# define SWIFT_AVAILABILITY(plat, ...) __attribute__((availability(plat, __VA_ARGS__)))
#endif
#if !defined(SWIFT_WEAK_IMPORT)
# define SWIFT_WEAK_IMPORT __attribute__((weak_import))
#endif
#if !defined(SWIFT_DEPRECATED)
# define SWIFT_DEPRECATED __attribute__((deprecated))
#endif
#if !defined(SWIFT_DEPRECATED_MSG)
# define SWIFT_DEPRECATED_MSG(...) __attribute__((deprecated(__VA_ARGS__)))
#endif
#if !defined(SWIFT_DEPRECATED_OBJC)
# if __has_feature(attribute_diagnose_if_objc)
#  define SWIFT_DEPRECATED_OBJC(Msg) __attribute__((diagnose_if(1, Msg, "warning")))
# else
#  define SWIFT_DEPRECATED_OBJC(Msg) SWIFT_DEPRECATED_MSG(Msg)
# endif
#endif
#if defined(__OBJC__)
#if !defined(IBSegueAction)
# define IBSegueAction 
#endif
#endif
#if !defined(SWIFT_EXTERN)
# if defined(__cplusplus)
#  define SWIFT_EXTERN extern "C"
# else
#  define SWIFT_EXTERN extern
# endif
#endif
#if !defined(SWIFT_CALL)
# define SWIFT_CALL __attribute__((swiftcall))
#endif
#if !defined(SWIFT_INDIRECT_RESULT)
# define SWIFT_INDIRECT_RESULT __attribute__((swift_indirect_result))
#endif
#if !defined(SWIFT_CONTEXT)
# define SWIFT_CONTEXT __attribute__((swift_context))
#endif
#if !defined(SWIFT_ERROR_RESULT)
# define SWIFT_ERROR_RESULT __attribute__((swift_error_result))
#endif
#if defined(__cplusplus)
# define SWIFT_NOEXCEPT noexcept
#else
# define SWIFT_NOEXCEPT 
#endif
#if !defined(SWIFT_C_INLINE_THUNK)
# if __has_attribute(always_inline)
# if __has_attribute(nodebug)
#  define SWIFT_C_INLINE_THUNK inline __attribute__((always_inline)) __attribute__((nodebug))
# else
#  define SWIFT_C_INLINE_THUNK inline __attribute__((always_inline))
# endif
# else
#  define SWIFT_C_INLINE_THUNK inline
# endif
#endif
#if defined(_WIN32)
#if !defined(SWIFT_IMPORT_STDLIB_SYMBOL)
# define SWIFT_IMPORT_STDLIB_SYMBOL __declspec(dllimport)
#endif
#else
#if !defined(SWIFT_IMPORT_STDLIB_SYMBOL)
# define SWIFT_IMPORT_STDLIB_SYMBOL 
#endif
#endif
#if defined(__OBJC__)
#if __has_feature(objc_modules)
#if __has_warning("-Watimport-in-framework-header")
#pragma clang diagnostic ignored "-Watimport-in-framework-header"
#endif
@import CoreBluetooth;
@import Foundation;
@import ObjectiveC;
#endif

#import "/Users/wld/Documents/Mudra/SDK.iOS/SDK.iOS/Mudra-Bridging-Header.h"

#endif
#pragma clang diagnostic ignored "-Wproperty-attribute-mismatch"
#pragma clang diagnostic ignored "-Wduplicate-method-arg"
#if __has_warning("-Wpragma-clang-attribute")
# pragma clang diagnostic ignored "-Wpragma-clang-attribute"
#endif
#pragma clang diagnostic ignored "-Wunknown-pragmas"
#pragma clang diagnostic ignored "-Wnullability"
#pragma clang diagnostic ignored "-Wdollar-in-identifier-extension"

#if __has_attribute(external_source_symbol)
# pragma push_macro("any")
# undef any
# pragma clang attribute push(__attribute__((external_source_symbol(language="Swift", defined_in="Mudra",generated_declaration))), apply_to=any(function,enum,objc_interface,objc_category,objc_protocol))
# pragma pop_macro("any")
#endif

#if defined(__OBJC__)

SWIFT_CLASS("_TtC5Mudra10BleService")
@interface BleService : NSObject
- (nonnull instancetype)init SWIFT_UNAVAILABLE;
+ (nonnull instancetype)new SWIFT_UNAVAILABLE_MSG("-init is unavailable");
@end

@class CBCentralManager;
@class CBPeripheral;
@class NSString;
@class NSNumber;

@interface BleService (SWIFT_EXTENSION(Mudra)) <CBCentralManagerDelegate>
- (void)centralManagerDidUpdateState:(CBCentralManager * _Nonnull)central;
- (void)centralManager:(CBCentralManager * _Nonnull)central didDiscoverPeripheral:(CBPeripheral * _Nonnull)peripheral advertisementData:(NSDictionary<NSString *, id> * _Nonnull)advertisementData RSSI:(NSNumber * _Nonnull)RSSI;
- (void)centralManager:(CBCentralManager * _Nonnull)central didDisconnectPeripheral:(CBPeripheral * _Nonnull)peripheral error:(NSError * _Nullable)error;
- (void)centralManager:(CBCentralManager * _Nonnull)central connectionEventDidOccur:(CBConnectionEvent)event forPeripheral:(CBPeripheral * _Nonnull)peripheral;
@end

@class CBService;
@class CBCharacteristic;

@interface BleService (SWIFT_EXTENSION(Mudra)) <CBPeripheralDelegate>
- (void)centralManager:(CBCentralManager * _Nonnull)central didConnectPeripheral:(CBPeripheral * _Nonnull)peripheral;
- (void)peripheral:(CBPeripheral * _Nonnull)peripheral didDiscoverServices:(NSError * _Nullable)error;
- (void)peripheral:(CBPeripheral * _Nonnull)peripheral didDiscoverCharacteristicsForService:(CBService * _Nonnull)service error:(NSError * _Nullable)error;
- (void)peripheral:(CBPeripheral * _Nonnull)peripheral didUpdateNotificationStateForCharacteristic:(CBCharacteristic * _Nonnull)characteristic error:(NSError * _Nullable)error;
- (void)peripheral:(CBPeripheral * _Nonnull)peripheral didWriteValueForCharacteristic:(CBCharacteristic * _Nonnull)characteristic error:(NSError * _Nullable)error;
- (void)peripheral:(CBPeripheral * _Nonnull)peripheral didUpdateValueForCharacteristic:(CBCharacteristic * _Nonnull)characteristic error:(NSError * _Nullable)error;
@end


SWIFT_CLASS("_TtC5Mudra21ConfigurationSettings")
@interface ConfigurationSettings : NSObject
- (nonnull instancetype)init OBJC_DESIGNATED_INITIALIZER;
@property (nonatomic) float gestureConfidenceThresholdUp;
@property (nonatomic) float gestureConfidenceThresholdDown;
@property (nonatomic) float pressureSmoothing;
@property (nonatomic) float airMouseThresholdUp;
@property (nonatomic) float airMouseThresholdDown;
@end

@class NSUUID;
@class MudraDevice;
@protocol MudraDelegate;

SWIFT_CLASS_NAMED("Mudra")
@interface MudraManager : NSObject
SWIFT_CLASS_PROPERTY(@property (nonatomic, class, readonly, strong) MudraManager * _Nonnull shared;)
+ (MudraManager * _Nonnull)shared SWIFT_WARN_UNUSED_RESULT;
SWIFT_CLASS_PROPERTY(@property (nonatomic, class, readonly) BOOL isValidKey;)
+ (BOOL)isValidKey SWIFT_WARN_UNUSED_RESULT;
SWIFT_CLASS_PROPERTY(@property (nonatomic, class, readonly, copy) NSArray<NSString *> * _Nullable allowedFeatures;)
+ (NSArray<NSString *> * _Nullable)allowedFeatures SWIFT_WARN_UNUSED_RESULT;
@property (nonatomic, readonly, copy) NSDictionary<NSUUID *, MudraDevice *> * _Nonnull devices;
@property (nonatomic, readonly, strong) ConfigurationSettings * _Nonnull configuration;
@property (nonatomic, strong) id <MudraDelegate> _Nullable delegate;
- (nonnull instancetype)init SWIFT_UNAVAILABLE;
+ (nonnull instancetype)new SWIFT_UNAVAILABLE_MSG("-init is unavailable");
+ (void)setKeyWithKey:(NSString * _Nonnull)key completion:(void (^ _Nonnull)(BOOL))completion;
+ (void)setLicense:(License)feature :(NSString * _Nonnull)licenseKey;
+ (void)setCoreLoggingSeverity:(LoggingSeverity)severity;
- (void)setFirmwareLoggingEnabledWithDevice:(MudraDevice * _Nonnull)device :(BOOL)enabled;
+ (LoggingSeverity)getLoggingSeverity SWIFT_WARN_UNUSED_RESULT;
- (void)setOnLoggingMsgBlock:(OnLoggingMsgBlock _Nullable)block;
- (void)scan;
- (void)stopScan;
- (NSArray<MudraDevice *> * _Nonnull)getDevices SWIFT_WARN_UNUSED_RESULT;
- (void)clear;
- (void)connectWithDevice:(MudraDevice * _Nonnull)device;
- (void)disconnectWithDevice:(MudraDevice * _Nonnull)device;
- (void)sendFirmwareCommandWithDevice:(MudraDevice * _Nonnull)device commmandBytes:(NSArray<NSNumber *> * _Nonnull)commmandBytes;
- (void)setIsAirMouseActiveWithDevice:(MudraDevice * _Nonnull)device active:(BOOL)active;
- (void)sendButtonsWithDevice:(MudraDevice * _Nonnull)device buttonsByte:(uint8_t)buttonsByte;
- (void)sendKeyboardWithDevice:(MudraDevice * _Nonnull)device key:(NSArray<NSNumber *> * _Nonnull)key;
- (void)sendModelTypeWithDevice:(MudraDevice * _Nonnull)device modelType:(uint8_t)modelType;
- (void)sendHandTypeWithDevice:(MudraDevice * _Nonnull)device handType:(uint8_t)handType;
- (void)resetAirMouseWithDevice:(MudraDevice * _Nonnull)device dimensions:(NSArray<NSNumber *> * _Nonnull)dimensions;
- (void)stopAdvertisingWithDevice:(MudraDevice * _Nonnull)device;
- (void)setObjectScaleWithDevice:(MudraDevice * _Nonnull)device objectScale:(uint8_t)objectScale;
- (void)recenterImuCubicWithDevice:(MudraDevice * _Nonnull)device;
- (void)sendAirMouseSpeedWithDevice:(MudraDevice * _Nonnull)device speed:(NSArray<NSNumber *> * _Nonnull)speed;
- (void)recieveCallWithPeripheral:(CBPeripheral * _Nonnull)peripheral;
- (void)dismissCallWithPeripheral:(CBPeripheral * _Nonnull)peripheral;
- (void)initBluetouth SWIFT_METHOD_FAMILY(none);
@end

@class NSData;

@interface MudraManager (SWIFT_EXTENSION(Mudra))
- (void)onBluetoothStateChanged:(BOOL)state;
- (void)onBatteryLevelChanged:(CBPeripheral * _Nonnull)peripheral level:(NSInteger)level;
- (void)onFirmwareVersionReceived:(CBPeripheral * _Nonnull)peripheral version:(NSString * _Nonnull)version;
- (void)onSerialNumberReceived:(CBPeripheral * _Nonnull)peripheral serialNumber:(NSInteger)serialNumber;
- (void)onScanFinished;
- (void)onPeripheralConnected:(CBPeripheral * _Nonnull)peripheral;
- (void)onPeripheralDisconnected:(CBPeripheral * _Nonnull)peripheral;
- (void)onSncPacketReceived:(CBPeripheral * _Nonnull)peripheral data:(NSData * _Nonnull)data;
- (void)onImuPacketReceived:(CBPeripheral * _Nonnull)peripheral data:(NSData * _Nonnull)data;
- (void)onSecondaryDeviceStateInitialized:(CBPeripheral * _Nonnull)peripheral;
- (void)onAirMouseCalibrationFinished:(CBPeripheral * _Nonnull)peripheral;
- (void)onStartCharging:(CBPeripheral * _Nonnull)peripheral;
- (void)onStopCharging:(CBPeripheral * _Nonnull)peripheral;
- (void)onMessageReceived:(CBPeripheral * _Nonnull)peripheral data:(NSData * _Nonnull)data;
- (void)onStopAdvertisingCallBack:(CBPeripheral * _Nonnull)peripheral;
- (void)onFirmwareCrachCallBack:(CBPeripheral * _Nonnull)peripheral;
- (void)updateUserData;
+ (NSString * _Nonnull)ToStringWithGesture:(Gesture)gesture SWIFT_WARN_UNUSED_RESULT;
+ (NSString * _Nonnull)ToStringWithCallHandlingAction:(CallHandlingAction)callHandlingAction SWIFT_WARN_UNUSED_RESULT;
+ (NSString * _Nonnull)ToStringWithMusicHandlingAction:(MusicHandlingAction)musicHandlingAction SWIFT_WARN_UNUSED_RESULT;
@end


SWIFT_PROTOCOL("_TtP5Mudra13MudraDelegate_")
@protocol MudraDelegate
@optional
- (void)onBluetoothStateChanged:(BOOL)state;
- (void)onMudraDeviceDiscovered:(MudraDevice * _Nonnull)device;
- (void)onDeviceConnectedByIos:(MudraDevice * _Nonnull)device;
- (void)onDeviceDisconnectedByIos:(MudraDevice * _Nonnull)device;
- (void)onMudraDeviceConnected:(MudraDevice * _Nonnull)device;
- (void)onMudraDeviceDisconnected:(MudraDevice * _Nonnull)device;
- (void)onBatteryLevelChanged:(MudraDevice * _Nonnull)device;
- (void)onFirmwareVersionChanged:(MudraDevice * _Nonnull)device;
- (void)onScanFinished;
@end


SWIFT_CLASS("_TtC5Mudra11MudraDevice")
@interface MudraDevice : NSObject
@property (nonatomic, strong) CBPeripheral * _Nonnull peripheral;
@property (nonatomic, copy) void (^ _Nullable onSncBlock)(uint32_t, NSArray<NSNumber *> * _Nonnull);
@property (nonatomic, copy) void (^ _Nullable onImuGyroBlock)(uint32_t, NSArray<NSNumber *> * _Nonnull);
@property (nonatomic, copy) void (^ _Nullable onImuAccRawBlock)(uint32_t, NSArray<NSNumber *> * _Nonnull);
@property (nonatomic, copy) void (^ _Nullable onImuAccNormBlock)(uint32_t, NSArray<NSNumber *> * _Nonnull);
@property (nonatomic, copy) void (^ _Nullable onImuQuaternionBlock)(uint32_t, NSArray<NSNumber *> * _Nonnull);
@property (nonatomic, copy) void (^ _Nullable onProportionalBlock)(float);
@property (nonatomic, copy) void (^ _Nullable onTensorFlowDataBlock)(NSArray<NSNumber *> * _Nonnull);
@property (nonatomic, copy) OnCalibrationFinishedBlock _Nullable oncalibrationChangedBlock;
@property (nonatomic, copy) void (^ _Nullable onGestureCalibrationWindowReadyBlock)(Gesture, WindowQuality);
@property (nonatomic, copy) void (^ _Nullable onAirMouseButtonChangedBlock)(uint8_t);
@property (nonatomic, copy) OnCalibrationFinishedBlock _Nullable onCalibrationChangedBlock;
@property (nonatomic, copy) OnGestureBlock _Nullable onGestureReadyBlock;
@property (nonatomic, copy) void (^ _Nullable onAirMouseCalibrationFinishedBlock)(void);
@property (nonatomic, copy) void (^ _Nullable onStartChargingBlock)(void);
@property (nonatomic, copy) void (^ _Nullable onStopChargingBlock)(void);
@property (nonatomic, copy) void (^ _Nullable onSecondaryDeviceStateInitializedBlock)(void);
@property (nonatomic, copy) void (^ _Nullable onSecondaryDeviceNameUpdatedBlock)(void);
@property (nonatomic, copy) void (^ _Nullable onStopAdvertisingCallBackBlock)(void);
@property (nonatomic, copy) void (^ _Nullable onFirmwareCrachCallBackBlock)(void);
@property (nonatomic, copy) void (^ _Nullable onMessageReceivedCallBackBlock)(NSData * _Nonnull);
@property (nonatomic, readonly) NSInteger id;
@property (nonatomic) NSInteger battery;
@property (nonatomic, copy) NSString * _Nullable firmwareVersion;
@property (nonatomic, copy) NSString * _Nullable bandNumber;
@property (nonatomic, readonly, copy) NSUUID * _Nonnull identifier;
@property (nonatomic, readonly) BOOL isConnected;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@property (nonatomic, readonly, copy) NSString * _Nullable peripheralName;
@property (nonatomic, readonly, copy) NSString * _Nullable uuid;
- (void)handleDataWithData:(NSData * _Nonnull)data;
- (void)logIsFunctionEnabledWithFunctionName:(NSString * _Nonnull)functionName isEnabled:(BOOL)isEnabled;
- (void)setOnSncPackageReady:(void (^ _Nullable)(uint32_t, NSArray<NSNumber *> * _Nonnull))block;
- (void)setOnMessageReceived:(void (^ _Nullable)(NSData * _Nonnull))block;
- (void)setOnSecondaryDeviceStateInitialized:(void (^ _Nullable)(void))block;
- (void)setOnSecondaryDeviceNameUpdated:(void (^ _Nullable)(void))block;
- (void)setOnStopAdvertisingCallBack:(void (^ _Nullable)(void))block;
- (void)setOnFirmwareCrashCallBack:(void (^ _Nullable)(void))block;
- (void)setOnProportionalReady:(void (^ _Nullable)(float))block;
- (void)setOnTensorFlowDataReady:(void (^ _Nullable)(NSArray<NSNumber *> * _Nonnull))block;
- (void)setOnGestureReady:(OnGestureBlock _Nullable)block;
- (void)setOnImuQuaternionReady:(void (^ _Nullable)(uint32_t, NSArray<NSNumber *> * _Nonnull))block;
- (void)setOnImuAccNormPackageReady:(void (^ _Nullable)(uint32_t, NSArray<NSNumber *> * _Nonnull))block;
- (void)setImuAccRawReady:(void (^ _Nullable)(uint32_t, NSArray<NSNumber *> * _Nonnull))block;
- (void)setImuGyroReady:(void (^ _Nullable)(uint32_t, NSArray<NSNumber *> * _Nonnull))block;
- (void)setOnAirMouseButtonChanged:(void (^ _Nullable)(uint8_t))block;
- (void)enableRecording;
- (void)startRecordingWithUser:(NSString * _Nonnull)user description:(NSString * _Nonnull)description;
- (void)stopRecording;
- (void)deleteLastRecording;
- (void)clearRecording;
- (NSString * _Nonnull)getJsonRecording SWIFT_WARN_UNUSED_RESULT;
- (NSString * _Nonnull)getCsvRecording SWIFT_WARN_UNUSED_RESULT;
- (void)setAirMouseActiveWithActive:(BOOL)active;
- (void)resetAirMouseWithDimensions:(NSArray<NSNumber *> * _Nonnull)dimensions;
- (void)stopAdvertising;
- (void)setObjectScaleWithObjectScale:(uint8_t)objectScale;
- (void)recenterImuCubic;
- (void)setOnCalibrationFinished:(OnCalibrationFinishedBlock _Nullable)block;
- (void)setOnCalibrationChanged:(void (^ _Nullable)(Gesture, WindowQuality))block;
- (void)onSecondaryDeviceStateInitialized;
- (void)setOnAirMouseCalibrationFinished:(void (^ _Nullable)(void))block;
- (void)setOnStartCharging:(void (^ _Nullable)(void))block;
- (void)setOnStopCharging:(void (^ _Nullable)(void))block;
- (void)onAirMouseCalibrationFinished;
- (void)onStartCharging;
- (void)onStopCharging;
- (void)recieveCall;
- (void)dismissCall;
- (void)clearSecondaryDevices;
@property (nonatomic, copy) NSString * _Nonnull currentCalibration;
- (void)setCalibration:(NSString * _Nonnull)jsonString;
- (void)clear;
- (void)clearGestureCalibration;
- (void)refineGestureCalibration;
- (void)clearGestureCalibration:(Gesture)gesture;
- (float)getCalibrationProgress SWIFT_WARN_UNUSED_RESULT;
- (void)deleteLastGestureEmbedding;
- (void)calibrateGesture:(Gesture)gesture;
- (void)calibratePressure:(PresureCalibrationMode)mode;
@property (nonatomic) BOOL gestureCalibrationEnabled;
@property (nonatomic) BOOL pressureCalibrationEnabled;
- (void)setFirmwareLoggingEnabled:(BOOL)enabled;
- (void)connect;
- (void)disconncet;
- (void)sendFirmwareCommandWithCommmand:(NSArray<NSNumber *> * _Nonnull)commmand;
@property (nonatomic, readonly) BOOL isGestureCalibrationReady;
@property (nonatomic) AlgorithemMode algorithemMode;
- (void)setAirMouseSpeedWithSpeed:(NSArray<NSNumber *> * _Nonnull)speed;
- (void)updateConnectionProperties;
- (void)updateDeviceDisconnectionProperties;
- (void)updateHandType;
- (void)onMessageReceivedWithData:(NSData * _Nonnull)data;
- (void)onStopAdvertisingCallBack;
- (void)onFirmwareCrachCallBack;
- (BOOL)isFinishedInitializing SWIFT_WARN_UNUSED_RESULT;
@property (nonatomic, readonly, copy) NSString * _Nonnull modelVersion;
- (nonnull instancetype)init SWIFT_UNAVAILABLE;
+ (nonnull instancetype)new SWIFT_UNAVAILABLE_MSG("-init is unavailable");
@end

#endif
#if __has_attribute(external_source_symbol)
# pragma clang attribute pop
#endif
#if defined(__cplusplus)
#endif
#pragma clang diagnostic pop
#endif

#else
#error unsupported Swift architecture
#endif
