# How to use
- Annotate your config class properties with attributes from `System.ComponentModel.DataAnnotations`:
 
  ```csharp
  public class MyConfiguration
  {
      [Required]
      public string Name { get; set; }

      [Range(0, 10)]
      public int Value { get; set; }
  }
  ```
- Add config:

  ```csharp
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddConfig<MyConfiguration>(Configuration, "MyConfig");
  }
  ```
  This code will throw if settings don't meet requirements
- Use configuration object in code:

  ```csharp
  public class SomeService : ISomeService
  {
      public SomeService(MyConfiguration config)
      {
      }
  }
  ```

# How not to use

This simple validation doesn't handle collections and complex object:
```csharp
class Config
{
   public Person Person { get; set; }
   public IEnumerable<Person> People { get; set; }
}

class Person
{
    [Required]
    public string Name { get; set; }
}
```

This configuration will not throw:
```json
{
    "Config":{
        "Person": {},
        "People":[
            {}
        ]
    }
}
```
