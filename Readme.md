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
  The validation throws during start up if something is wrong with the configuration
- Use configuration object in code:

  ```csharp
  public class SomeService : ISomeService
  {
      public SomeService(IOptions<MyConfiguration> config)
      {
      }
  }
  ```

