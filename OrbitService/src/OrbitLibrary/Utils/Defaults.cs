using OrbitLibrary.Common;
using OrbitLibrary.Data;
using OrbitLibrary.Infrastructure.Data;
using OrbitLibrary.Infrastructure.Repositories;
using System.Collections.Generic;

namespace OrbitLibrary.Utils
{
    public class Defaults
    {

        public static CredentialsProvider GetCredentialsProvider(ServiceConfiguration sConfig, CommunicationProvider client)
        {
            return new OrbitCredentialProvider(sConfig, client);
        }

        public static CommunicationProvider GetCommunicationProvider()
        {
            return new HttpClientCommunicationProvider();
        }

        public static IWrapper GetWrapper()
        {
            AppSettings appSettings = AppSettings.GetAppSettings();
            ODBCDbFactory factory = new ODBCDbFactory(appSettings.ConnectionString);
            return new DbWrapper(factory);
        }
        public static ServiceDependencies GetServiceDependencies()
        {
            IWrapper wrapper = GetWrapper();
            DBServiceConfigurationRepository serviceRepo = new DBServiceConfigurationRepository(wrapper);
            ServiceConfiguration sConfig = serviceRepo.GetConfiguration();
            CommunicationProvider communicationProvider = GetCommunicationProvider();
            sConfig.CredentialsProvider = GetCredentialsProvider(sConfig, communicationProvider);
            return new ServiceDependencies(sConfig, communicationProvider, wrapper, "") ;
        }

        public static List<ServiceDependencies> GetListServiceDependencies()
        {
            List<AppSettings> ListappSettings = AppSettings.GetListAppSettings();
            List<ServiceDependencies> listServiceDependencies = new List<ServiceDependencies>();
            foreach (AppSettings item in ListappSettings)
            {
                ODBCDbFactory factory = new ODBCDbFactory(item.ConnectionString);
                IWrapper wrapper = new DbWrapper(factory);
                DBServiceConfigurationRepository serviceRepo = new DBServiceConfigurationRepository(wrapper);
                ServiceConfiguration sConfig = serviceRepo.GetConfiguration();

                CommunicationProvider communicationProvider = GetCommunicationProvider();
                sConfig.CredentialsProvider = GetCredentialsProvider(sConfig, communicationProvider);
                listServiceDependencies.Add(new ServiceDependencies(sConfig, communicationProvider, wrapper,item.DataBaseName));
            }
            return listServiceDependencies;
        }
    }
}
