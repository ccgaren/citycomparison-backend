using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityComparison.Api
{
    public class ActionWithMessageResult : ActionResult
    {
        private string _message;

        public ActionWithMessageResult(string message)
        {
            _message = message;
        }
    }
}
