# Rtl.Configuration.Validation
Rtl.Configuration.Validation is a package with an extention method for `IServiceCollection` that adds config class and binds it to a section from `IConfiguration`:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Instead of this line:
    // services.Configure<MyConfiguration>(Configuration.GetSection("MyConfig"));
    // use this one:
    services.AddConfig<MyConfiguration>(Configuration, "MyConfig");
}
```

The benefit over normal `services.Configure` is that you can annotate config class with attributes from [`System.ComponentModel.DataAnnotations`](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations):
```csharp
public class MyConfiguration
{
    [Required]
    public string Name { get; set; }

    [Range(0, 10)]
    public int Value { get; set; }
}
  ```
Now if your configuration doesn't meet requirements set by these attributes the code will throw.
It will throw after `ConfigureServices` method but before `Configure` method so you can be sure that everything is all right before pipeline starts.

Complex objects and collections are also supported

#### Example
```csharp
public class Config
{
    [Required]
    public string Id { get; set; }
    public Person Person { get; set; }
    public IEnumerable<Person> People { get; set; }
}

public class Person
{
    [Required]
    public string Name { get; set; }
    [Range(1, 100)]
    public int Age { get; set; }
}
```

Given this definition of `Config` class the code `services.AddConfig<Config>(Configuration, "MyConfig");` will throw if:

- `Config.Id` is empty
- `Person.Name` is not set or empty in `Config.Person` object or in any item from `Config.People` collection
- `Person.age` is default (zero) or any other outside range from 1 to 100

Note that `Config.Person` and `Config.People` properties can be null because they are not required

# Api

#### AddConfig
```csharp
public static IServiceCollection AddConfig<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
    where T : class, new()
```
Adds IOptions\<T> to IoC container, validates config before `Startup.Configure` is called

#### GetConfig
```csharp
public static T GetConfig<T>(this IConfiguration configuration, string sectionName)
    where T : class, new()
```
Gets config of type `T` from configuration, validates and returns it

