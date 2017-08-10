# simpleinjector.extensions
Adds Extensions Helpers to Simple Injector (https://simpleinjector.org)

Current Container Extensions:
- RegisterImplementationsFromAssembly

    Batch or automatic registration is a way of registering a set of (related) types in one go based on some convention. This feature removes the need to constantly update the container’s configuration each and every time a new type is added. The following example show a series of manually registered repositories:

      container.Register<IUserRepository, SqlUserRepository>();
      container.Register<ICustomerRepository, SqlCustomerRepository>();
      container.Register<IOrderRepository, SqlOrderRepository>();
      container.Register<IProductRepository, SqlProductRepository>();
      // and the list goes on...

    RegisterImplementationsFromAssembly is a feature that brings the easiness of automatically registering types for an entire assembly.
    
    Usage:

    Decorate classes(implementations) with the attribute '[DependencyInjection()]'
    
    Example:
    
      [DependencyInjection(typeof(IProductService))]
      public class ProductService : IProductService
      {
      }
      
      [DependencyInjection(typeof(IUserService))]
      public class UserService : IUserService
      {
      }
      
    Register using the container extension function (the assembly must be the assembly of the classes(implementations)):
    
      // implementations(classes) and contracts(interfaces) can be in different assemblies. Only implementation assemblies must be informed
      container.RegisterImplementationsFromAssembly(assembly, Lifestyle.Scoped);
