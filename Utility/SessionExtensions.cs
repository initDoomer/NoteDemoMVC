using Microsoft.AspNetCore.Http;
using System.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace NoteDemoMVC.Utility
{
    public static class SessionExtensions
    {
        // these are the extension methods need if we want to store complex 
        // objects in the session
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }


        public static T  Get<T>(this ISession session, string key)
        {

            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }



    }
}
