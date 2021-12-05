namespace FirstLineSoftwareInterviewTask.Common.Core.Settings.DataAccess
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        DatabaseCollections DatabaseCollections { get; set; }
    }
}