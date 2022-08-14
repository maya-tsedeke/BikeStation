using Project.ApplicationCore.DTOs;
namespace Project.ApplicationCore.Interfaces
{
    public interface ICSVDataImportRepository
    {
        CSVFileResponse ImportBikeStationList(CSVFileRequest request,string path);
    }
}
