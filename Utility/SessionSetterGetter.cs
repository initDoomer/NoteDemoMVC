using Microsoft.AspNetCore.Http;
using NoteDemoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC.Utility
{
    public class SessionSetterGetter
    {

        public static IEnumerable<UrgentNote> GetSession(HttpContext context, string sessionName)
        {

            return context.Session.Get<IEnumerable<UrgentNote>>(sessionName);

        }

        public static void SetSession(HttpContext context, string sessionName, List<UrgentNote> noteList)        {

            context.Session.Set(sessionName, noteList);
        }
    }
}
