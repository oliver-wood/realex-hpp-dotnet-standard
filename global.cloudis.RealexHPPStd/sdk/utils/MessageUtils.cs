using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.sdk.utils
{
    public static class MessageUtils
    {
        public static string GetMessageForResult(string result)
        {
            ResourceManager rm = global.cloudis.RealexHPP.HPPMessages.ResourceManager;
            
            if (!string.IsNullOrEmpty(rm.GetString("hppResponse_message_" + result)))
            {
                return rm.GetString("hppResponse_message_" + result);
            }
            else
            {
                string resultType = result.Substring(0, 1) + "xx";
                if (!string.IsNullOrEmpty(rm.GetString("hppResponse_message_" + resultType)))
                {
                    return rm.GetString("hppResponse_message_" + resultType);
                }
                else
                {
                    return rm.GetString("hppResponse_message_unknown");
                }
            }
        }

        public static string GetMessage(string key)
        {
            ResourceManager rm = global.cloudis.RealexHPP.HPPMessages.ResourceManager;
            return rm.GetString(key);
        }
    }

}
