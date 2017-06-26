﻿using System.Linq;
using System.Web;
using ServiceStack;
using Autoquery.ServiceModel;

namespace Autoquery.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }
    }
}