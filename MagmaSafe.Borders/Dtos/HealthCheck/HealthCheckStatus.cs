using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MagmaSafe.Borders.Dtos.HealthCheck
{
    public class HealthCheckStatus
    {
        public HealthCheckStatus()
        {
            Activities = new List<ActivitieHealthCheck>();
            Version = GetType()
                     .GetTypeInfo()
                     .Assembly
                     .GetCustomAttribute<AssemblyFileVersionAttribute>()
                     .Version;
        }

        public string Version { get; }

        public List<ActivitieHealthCheck> Activities { get; }

        public bool Success => Activities.All(a => a.Success);
    }

    public class ActivitieHealthCheck
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
