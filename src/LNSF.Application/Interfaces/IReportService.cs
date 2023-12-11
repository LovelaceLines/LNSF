namespace LNSF.Application.Interfaces;

public interface IReportService
{
    byte[] ExportReport<T>(List<T> list, string fileNameFRX, string referenceName);
}
