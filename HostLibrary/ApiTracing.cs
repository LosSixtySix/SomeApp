using System.Diagnostics;

namespace HostLibrary
{
    public static class ApiTracing
    {
        public const string SourceName = "SomeApp.Api";
        public static ActivitySource Source = new(SourceName);
    }
}
