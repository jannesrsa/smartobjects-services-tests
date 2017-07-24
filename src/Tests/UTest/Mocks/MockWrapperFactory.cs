﻿using Moq;
using SourceCode.Deployment.Management;
using SourceCode.EnvironmentSettings.Client;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.UTest.Mocks
{
    internal class MockWrapperFactory
    {
        public MockWrapperFactory()
        {
            Factory = new Mock<WrapperFactory>();

            SmartObjectClientServer = new Mock<SmartObjectClientServerWrapper>();
            SmartObjectManagementServer = new Mock<SmartObjectManagementServerWrapper>();
            ServiceManagementServer = new Mock<ServiceManagementServerWrapper>();
            EnvironmentSettingsManager = new Mock<EnvironmentSettingsManagerWrapper>();
            PackageDeploymentManager = new Mock<PackageDeploymentManagerWrapper>();
            WebRequestManager = new Mock<WebRequestWrapper>();
            SecurityManager = new Mock<SecurityWrapper>();

            Factory.Setup(x => x.GetSmartObjectClientServerWrapper(It.IsAny<SmartObjectClientServer>())).Returns(SmartObjectClientServer.Object);
            Factory.Setup(x => x.GetSmartObjectManagementServerWrapper(It.IsAny<SmartObjectManagementServer>())).Returns(SmartObjectManagementServer.Object);
            Factory.Setup(x => x.GetServiceManagementServerWrapper(It.IsAny<ServiceManagementServer>())).Returns(ServiceManagementServer.Object);
            Factory.Setup(x => x.GetEnvironmentSettingsManagerWrapper(It.IsAny<EnvironmentSettingsManager>())).Returns(EnvironmentSettingsManager.Object);
            Factory.Setup(x => x.GetPackageDeploymentManagerWrapper(It.IsAny<PackageDeploymentManager>())).Returns(PackageDeploymentManager.Object);
            Factory.Setup(x => x.GetWebRequestWrapper()).Returns(WebRequestManager.Object);
            Factory.Setup(x => x.GetSecurityWrapper()).Returns(SecurityManager.Object);

            Factory.Setup(x => x.GetServer<SmartObjectClientServer>()).Returns(new SmartObjectClientServer());
            Factory.Setup(x => x.GetServer<SmartObjectManagementServer>()).Returns(new SmartObjectManagementServer());
            Factory.Setup(x => x.GetServer<ServiceManagementServer>()).Returns(new ServiceManagementServer());
            Factory.Setup(x => x.GetServer<PackageDeploymentManager>()).Returns(new PackageDeploymentManager());

            WrapperFactory.Instance = Factory.Object;
        }

        public Mock<EnvironmentSettingsManagerWrapper> EnvironmentSettingsManager { get; }

        public Mock<WrapperFactory> Factory { get; }

        public Mock<PackageDeploymentManagerWrapper> PackageDeploymentManager { get; private set; }

        public Mock<SecurityWrapper> SecurityManager { get; }

        public Mock<ServiceManagementServerWrapper> ServiceManagementServer { get; }

        public Mock<SmartObjectClientServerWrapper> SmartObjectClientServer { get; }

        public Mock<SmartObjectManagementServerWrapper> SmartObjectManagementServer { get; }

        public Mock<WebRequestWrapper> WebRequestManager { get; }
    }
}