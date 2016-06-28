﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace NormalizedSystems.Net
{
    public static class Utils
    {
        public static T Cast<T>(this object data)
        {
            return (T) typeof(T).Cast(data);
        }

        public static object Cast(this Type type, object data)
        {
            if (data == null) return null;

            var DataParam = Expression.Parameter(typeof(object), "data");
            var Body = Expression.Block(Expression.Convert(Expression.Convert(DataParam, data.GetType()), type));

            var Run = Expression.Lambda(Body, DataParam).Compile();
            var ret = Run.DynamicInvoke(data);
            return ret;
        }

        public static T Clone<T>(this T obj)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj, settings), settings);
        }
    }
}
