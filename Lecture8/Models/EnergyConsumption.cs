namespace Lecture8.Models;

public class EnergyConsumption
{
    public int Year { get; set; }
    public string Month { get; set; }
    public string State { get; set; }
    public string ProducerId { get; set; }
    public string Consumption { get; set; }
    public string EnergySourceUnits { get; set; }

    public override string ToString()
    {
        return $"Year: {Year}, Month: {Month}, State: {State}, Consumption: {Consumption}";
    }
}