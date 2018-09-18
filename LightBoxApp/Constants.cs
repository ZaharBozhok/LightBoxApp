using System;
using Xamarin.Forms;

namespace LightBoxApp
{
    public class Constants
    {
        public static Color OnColor = Color.FromHex("#B7AE20");
        public static Color OffColor = Color.FromHex("#38383A");
        public static Color BorderColor = Color.White;
        public static Color ButtonBackColor = Color.White;

        public static string AddressOnAP = "http://192.168.4.1";
        public static string ConfigsPath = "/configs";
        public static string ControlPath = "control";

        public static string DevicesKey = "Devices";
        public static string PresetsKey = "Presets";

        public static int XAmount = 5;
        public static int YAmount = 5;

        public static string DefaultLightBoxName = "LightBox";

        public static TimeSpan HttpRequestTimeout = new TimeSpan(0, 0, 5);
    }
}
