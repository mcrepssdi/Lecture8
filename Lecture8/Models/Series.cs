namespace Lecture8.Models;

public class Series
{
    public string Series_reference { get; set; } = string.Empty;
    public double Period { get; set; } = 0.0;
    public string Data_value { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
    public string Units { get; set; } = string.Empty;
    public string Magntude { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string Series_title_1 { get; set; } = string.Empty;
    public string Series_title_2 { get; set; } = string.Empty;
    public string Series_title_3 { get; set; } = string.Empty;
    public string Series_title_4 { get; set; } = string.Empty;
    public string Series_title_5 { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; } = new();
}