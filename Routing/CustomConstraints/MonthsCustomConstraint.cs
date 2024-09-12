﻿
using System.Text.RegularExpressions;

namespace Routing.CustomConstraints
{
    public class MonthsCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            //check if in the value exist
            if (!values.ContainsKey(routeKey)) //month
            {
                return false; //not a match
            }

            Regex regex = new Regex($"^(apr|jul|oct|jan)");
            string? monthValue = Convert.ToString(values[routeKey]);

            if(regex.IsMatch(monthValue)) { return true; }
            return false;
        }
    }
}
