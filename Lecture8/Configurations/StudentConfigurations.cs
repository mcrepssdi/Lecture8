using Lecture8.Configurations.Sections;

namespace Lecture8.Configurations;

public class StudentConfigurations : IUtConfiguration
{
    public AppEnvironment AppEnvironment { get; set; } = new(); 
}