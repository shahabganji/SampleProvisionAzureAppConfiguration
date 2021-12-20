using Pulumi;
using Pulumi.Azure.AppConfiguration;
using Pulumi.Azure.Core;

namespace SampleProvisionAzureAppConfiguration
{
    public class ProvisionAppConfiguration : Stack
    {
        public ProvisionAppConfiguration()
        {
            var resourceGroup = new ResourceGroup("rg-sample");

            var appConfig = new ConfigurationStore("conf-sample", new ConfigurationStoreArgs
            {
                ResourceGroupName = resourceGroup.Name,
                Sku = "standard"
            });

            _ = new Pulumi.AzureNative.AppConfiguration.KeyValue("AnotherFeatureFlag",
                new Pulumi.AzureNative.AppConfiguration.KeyValueArgs
                {
                    ResourceGroupName = resourceGroup.Name,
                    ConfigStoreName = appConfig.Name,
                    KeyValueName = ".appconfig.featureflag~2FAnotherFeatureFlag",
                    ContentType = "application/vnd.microsoft.appconfig.ff+json;charset=utf-8",
                    Value = @"{""id"": ""AnotherFeatureFlag"",""description"": """",""enabled"": true,""conditions"": {""client_filters"": []}}"
                });
        }
    }
}
