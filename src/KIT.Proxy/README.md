# KIT.Proxy

This assembly is designed to use proxy wrappers.

## Registration and setup.
This assembly uses another KIT assembly - **KIT.Redis**.
Configure the proxy using the **ProxyConfigurator.ConfigureProxy** method.

## Using the assembly features.

For example, using assembly features to cache method data:
	1. Set the **UseCache** attribute to your method.
	2. Register your class to have an interface or virtual method with an attribute.
	You can use the **ProxyConfigurator.AddProxiedCacheScoped** method to do that.

	Now your method will be executed when needed. And the data returned by the method will be cached.


## Good luck!