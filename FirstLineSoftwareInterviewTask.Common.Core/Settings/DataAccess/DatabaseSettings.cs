namespace FirstLineSoftwareInterviewTask.Common.Core.Settings.DataAccess
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public DatabaseCollections DatabaseCollections { get; set; }
    }

    public class DatabaseCollections
    {
        public string UserCollectionName { get; set; }
        public string ItemCollectionName { get; set; }
    }
}