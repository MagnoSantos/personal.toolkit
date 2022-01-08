using System;
using System.Reflection;

namespace Personal.Commom
{
    public static class CreatorHelper
    {
        private static readonly string _applicationIdentity = Assembly.GetEntryAssembly().GetName().Name;
        private static readonly string _systemUser = Environment.UserName;
        private static readonly string _hostName = Environment.MachineName;

        public static string GetCreator => $"{_systemUser}@{_hostName} ({_applicationIdentity})";
    }
}