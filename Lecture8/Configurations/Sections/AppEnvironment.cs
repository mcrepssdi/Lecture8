namespace Lecture8.Configurations.Sections;

public class AppEnvironment
{
    public string ApplicationName { get; set; } = string.Empty;
    public string AppServer { get; set; } = string.Empty;
    public string AppDb { get; set; } = string.Empty;
    public string ConnectionStr => $"Sever={AppServer};Database={AppDb};Trusted_Connection=True;";
}