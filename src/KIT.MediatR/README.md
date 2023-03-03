# KIT.MediatR

This assembly offers auxiliary components for working with the MediatR.

## Registration and setup.
This assembly uses other Kit assemblies - **Kit.Redis**, **Tolar.NLog**.

Request pipeline behavior setup:
	``` 
		services.RegisterPipelineBehaviors(typeof(LogPipelineBehavior<,>), attribute => attribute.UseLogging);
        services.RegisterPipelineBehaviors(typeof(ValidationPipelineBehavior<,>), attribute => attribute.UseValidation);
        services.RegisterPipelineBehaviors(typeof(CachePipelineBehavior<,>), attribute => attribute.UseCache);
	```
To use the **CachePipelineBehavior**, register a **IRequestSalt**.

## Using the assembly features.

After registering the request pipeline behaviors, set the **UsePipelineBehaviors** attribute on the request handlers.

## Good luck!