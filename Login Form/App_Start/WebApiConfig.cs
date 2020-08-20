using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Login_Form
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithValidate",
                routeTemplate: "api/{controller}/Validate_{user}&{pass}"
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithSignUp",
                routeTemplate: "api/{controller}/AddToList_{user}&{pass}"
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
            );
            config.Routes.MapHttpRoute(
              name: "DefaultApiWithPostChangePassword",
              routeTemplate: "api/{controller}/PostChangePassword/{user}&{oldPass}&{newPass}"
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithPostAddItemToUserCart",
                routeTemplate: "api/{controller}/PostAddItemToUserCart/{user}&{item}&{quant}"
            );
            config.Routes.MapHttpRoute(
              name: "DefaultApiWithRemoveUser",
              routeTemplate: "api/{controller}/PostRemoveUser/{user}&{auth}"
            );
            config.Routes.MapHttpRoute(
              name: "DefaultApiWithPostRemoveItemFromCart",
              routeTemplate: "api/{controller}/PostRemoveItemFromCart/{user}&{item}"
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithAdd",
                routeTemplate: "api/{controller}/{action}/{u}&{n}&{q}&{c}&{d}"
            );
            config.Routes.MapHttpRoute(
               name: "DefaultApiWithRemove",
               routeTemplate: "api/{controller}/{action}/{u}&{i}"
           );
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithGetAll",
                routeTemplate: "api/{controller}/{action}/{u}"
            );
        }
    }
}
