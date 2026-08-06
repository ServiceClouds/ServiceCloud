// Persistence/Configurations/ConfigurationScanner.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public static class ConfigurationScanner
    {
        // Master configurations are in "Persistence.Configurations.ServiceCloud" namespace
        private const string MasterConfigurationNamespace = "Persistence.Configurations.ServiceCloud";

        // Tenant configurations are in "Persistence.Configurations.Tenant" namespace
        private const string TenantConfigurationNamespace = "Persistence.Configurations.Tenant";

        /// <summary>
        /// Determines if a configuration belongs to Master database
        /// </summary>
        public static bool IsMasterConfiguration(Type configurationType)
        {
            // Check if the configuration is in the ServiceCloud folder/namespace
            return configurationType.Namespace?.Contains(MasterConfigurationNamespace) == true;
        }

        /// <summary>
        /// Determines if a configuration belongs to Tenant database
        /// </summary>
        public static bool IsTenantConfiguration(Type configurationType)
        {
            // Check if the configuration is in the Tenant folder/namespace
            return configurationType.Namespace?.Contains(TenantConfigurationNamespace) == true;
        }

        /// <summary>
        /// Alternative: Check by entity type namespace
        /// </summary>
        public static bool IsMasterEntity(Type configurationType)
        {
            return GetEntityType(configurationType)
                ?.Namespace?.Contains("Domain.Entities.ServiceCloud") == true;
        }

        /// <summary>
        /// Alternative: Check by entity type namespace
        /// </summary>
        public static bool IsTenantEntity(Type configurationType)
        {
            return GetEntityType(configurationType)
                ?.Namespace?.Contains("Domain.Entities.Tenant") == true;
        }

        private static Type? GetEntityType(Type configurationType)
        {
            return configurationType.GetInterfaces()
                .Where(i => i.IsGenericType &&
                           i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                .Select(i => i.GetGenericArguments()[0])
                .FirstOrDefault();
        }
    }
}