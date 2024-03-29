﻿using OrbitLibrary.Services.Session;
using System;
using System.Linq;

namespace OrbitLibrary.Common
{

    public class OrbitCredentialProvider : CredentialsProvider
    {
        private ServiceConfiguration sConfig;
        private CommunicationProvider Client { get; }

        private static SessionCredentials lastValidSessionCredentials;

        public OrbitCredentialProvider(ServiceConfiguration sConfig, CommunicationProvider client)
        {
            this.sConfig = sConfig;
            Client = client;
        }

        public SessionCredentials ResolveCredentials(bool forceNewAuthentication = false)
        {

            if (lastValidSessionCredentials != null)
            {
                if (this.sConfig.TenantID.ToString() != lastValidSessionCredentials.TenantId)
                {
                    return generateNewSessionCredentials();
                }
            }
            if (!forceNewAuthentication)
            {
                if (lastValidSessionCredentials != null && lastValidSessionCredentials.IsValid())
                {
                    return lastValidSessionCredentials;
                }
            }


            return generateNewSessionCredentials();
        }

        private SessionCredentials generateNewSessionCredentials()
        {
            Login serviceAuthentication = new Login(sConfig, Client);
            OperationResponse<LoginResponseOutput, ErrorOutput> response = serviceAuthentication.Execute();
            Tenant tenantObject = null;

            if (!response.isSuccessful)
            {
                return updateLastSessionCredentials(new SessionCredentials());
            }

            LoginResponseOutput authenticationOutput = response.GetSuccessResponse();

            if (authenticationOutput.data == null)
            {
                return updateLastSessionCredentials(new SessionCredentials());
            }

            if (authenticationOutput.data.tenants.Count > 0)
            {
                var selectedTenants = authenticationOutput
                    .data
                    .tenants
                    .Where(t => t.tenantId == sConfig.TenantID.ToString());

                tenantObject = selectedTenants.Count() > 0 ? selectedTenants.First() : null;
            }

            //OrbitService_COL_Fiscal.Utils.Log.InsertLog($"PK:{tenantObject.pk}");
            //OrbitService_COL_Fiscal.Utils.Log.InsertLog($"TENANT ORBIT:{tenantObject.tenantId}");
            //OrbitService_COL_Fiscal.Utils.Log.InsertLog($"TENANT NAME:{tenantObject.tenantName}");
            //OrbitService_COL_Fiscal.Utils.Log.InsertLog($"TENANT B1:{sConfig.TenantID}");

            return updateLastSessionCredentials(new SessionCredentials(tenantObject == null ? string.Empty : tenantObject.pk, authenticationOutput.token, tenantObject.tenantId));
        }

        private SessionCredentials updateLastSessionCredentials(SessionCredentials sessionCredentials)
        {
            lastValidSessionCredentials = sessionCredentials.IsValid() ? sessionCredentials : null;
            return sessionCredentials;
        }
    }
}
