
namespace Project.ApplicationCore.Interfaces
{
    public interface IReaderWrapperRepository
    {
        IGetStationRepository BikeStation { get; } 
        void Save();
    }
}
