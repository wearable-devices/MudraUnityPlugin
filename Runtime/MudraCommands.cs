
namespace Mudra.Unity
{
    public static class MudraConstants
    {
        //C# is stupid, no members in the global/namespace scope, and since array values cant be know at compie time we use static readonly instead of constant for arrays
        
        public static readonly byte[] AIRMOUSE_ON =             { 0x07, 0x07, 0x01 };
        public static readonly byte[] AIRMOUSE_OFF =            { 0x07, 0x07, 0x00 };
       
        //Airmouse speed has 1 byte for code 0x44 and then two bytes for x speed and two bytes for y speed, default is [5,5]
        public static readonly byte[] NAVIGATION_SPEED =          { 0x44, 0x00, 0x05, 0x00, 0x05 };

        public static readonly byte[] PRESSURE_SENS_LOW =       { 0x21, 0x30 };
        public static readonly byte[] PRESSURE_SENS_MIDLOW =    { 0x21, 0x40 };
        public static readonly byte[] PRESSURE_SENS_MID =       { 0x21, 0x60 };
        public static readonly byte[] PRESSURE_SENS_MIDHIGH =   { 0x21, 0x70 };
        public static readonly byte[] PRESSURE_SENS_HIGH =      { 0x21, 0x80 };

        public static readonly byte[] PRESSURE_SCALE_LOW =      { 0x20, 0x00 };
        public static readonly byte[] PRESSURE_SCALE_MID =      { 0x20, 0x01 };
        public static readonly byte[] PRESSURE_SCALE_HIGH =     { 0x20, 0x02 };

    
    }

   

}