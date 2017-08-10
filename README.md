# simpleinjector.extensions
Adds Extensions Helpers to Simple Injector (https://simpleinjector.org)

Batch or automatic registration is a way of registering a set of (related) types in one go based on some convention. This feature removes the need to constantly update the container’s configuration each and every time a new type is added. The following example show a series of manually registered repositories:

```
	container.Register<IUserRepository, SqlUserRepository>();
 	container.Register<ICustomerRepository, SqlCustomerRepository>();
 	container.Register<IOrderRepository, SqlOrderRepository>();
 	container.Register<IProductRepository, SqlProductRepository>();
 	// and the list goes on...
```
'RegisterByAttribute' and 'RegisterByConvention' are features that bring the easiness of automatically registering types for an entire assembly.
Everything is done by reflection. 

# Current Container Extensions:
## - RegisterByAttribute

All services are registered to implementations by using the 'DependencyInjection' decorator.
        
**Usage:**

Decorate classes(implementations) with the attribute '[DependencyInjection()]'
    
**Example:**
    
```
[DependencyInjection(typeof(IProductService))]
public class ProductService : IProductService
{
}
     
// Register using the container extension function (the assembly must be the assembly of the classes(implementations)):
container.RegisterByAttribute(assembly, Lifestyle.Scoped);
```
## - RegisterByConvention

All services are registered to implementations by using Convention over Configuration.
The naming convention is: '[Classname] : I[Classname]'.
    
**Usage:**

Name class(implementation) with the same name of the service(interface)

**Example:**

```
public class ProductService : IProductService
{
}
     
// Register using the container extension function (the assembly must be the assembly of the classes(implementations)):
container.RegisterByConvention(assembly, Lifestyle.Scoped);
```

# Instalation:

Package available on [Nuget Package Manager](https://www.nuget.org/packages/RTI.SimpleInjector.Extensions/). 
PM> Install-Package **RTI.SimpleInjector.Extensions**
	
