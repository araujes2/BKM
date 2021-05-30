

using System.Text;

namespace BKM.API
{
    public class ServiceOptions
    {
        public const string Configurations = "Configurations";
        public string SqlConnectionString { get; set; }
        public string StorageConnectionString { get; set; }
        public string QueueName { get; set; }

    }
}
