namespace TodoList.Core
{
    public class AppSettings
    {
        public ConnectionStringsMetadata ConnectionStrings { get; set; }
    }

    public class ConnectionStringsMetadata
    {
        public string AppConnection { get; set; }
    }
}
