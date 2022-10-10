using OrbitLibrary.Common;
using OrbitLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Utils
{
   public class ServiceDependencies
    {
        public ServiceConfiguration sConfig { get; }
        public CommunicationProvider communicationProvider { get; }
        public IWrapper DbWrapper { get; }

        public string DatabaseName { get; }

        public ServiceDependencies(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IWrapper wrapper, string DatabaseName)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            this.DatabaseName = DatabaseName;
            DbWrapper = wrapper;
        }
    }
}
