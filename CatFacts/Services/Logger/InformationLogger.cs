namespace CatFacts.Services.Logger;

public interface IInformationLogger
{
    public void Log(string message);
}

public class InformationLogger : IInformationLogger
{
    private readonly string _logFilePath;

    public InformationLogger(IWebHostEnvironment webHostEnvironment, IConfigurationRoot configurationRoot)
    {
        string combined = Path.Combine(webHostEnvironment.ContentRootPath, configurationRoot["CatFactsLoggerFileRelativePath"] ?? "");
        _logFilePath = Path.GetFullPath(combined);
    }

    public void Log(string message)
    {
        using (StreamWriter writer = new StreamWriter(_logFilePath, File.Exists(_logFilePath)))
        {
            writer.WriteLine(message);
        }
    }
}
