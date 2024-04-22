namespace Craftable.PostCodes.Application.Abstractions.Application
{
    public interface IGeoCalculatorService
    {
        double GetDistanceToHeathrow(double latitude, double longitude);
    }
}