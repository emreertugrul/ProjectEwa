using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.Common.Settings
{
    public static class SettingsManager
    {
        private const string _SETTING_IOTHUBDEVICEKEY = "IOTHubDeviceKey";
        private const string _SETTING_IOTHUBCONNECTIONSTRING = "IOTHubConnectionString";
        private const string _SETTING_IOTDEVICEID = "IOTDeviceId";

        // Get a LocalSettings reference
        private static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;


        public static string IOTDeviceId
        {
            get { return Get(_SETTING_IOTDEVICEID); }
            set { localSettings.Values[_SETTING_IOTDEVICEID] = value; }
        }
        public static string IOTHubDeviceKey
        {
            get { return Get(_SETTING_IOTHUBDEVICEKEY); }
            set { localSettings.Values[_SETTING_IOTHUBDEVICEKEY] = value; }
        }
        public static string IOTHubConnectionString
        {
            get { return Get(_SETTING_IOTHUBCONNECTIONSTRING); }
            set { localSettings.Values[_SETTING_IOTHUBCONNECTIONSTRING] = value; }
        }

        /// <summary>
        /// Gets a setting value based on key. Returns null if the key is not found.
        /// </summary>
        /// <param name="key">Key of the setting</param>
        /// <returns></returns>
        public static string Get(string key)
        {
            object val;
            localSettings.Values.TryGetValue(key, out val);
            return val == null ? null : val.ToString();
        }        
    }
}
