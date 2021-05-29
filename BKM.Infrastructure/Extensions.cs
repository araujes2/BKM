using Azure.Storage.Queues;
using BKM.Core.Commands;
using BKM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BKM.Infrastructure
{
    public static class Extensions
    {
        public static async Task SendMessageAsync(this ICreateAuthorHandler source, string connectionString)
        {
            var queue = new QueueClient(connectionString, "mystoragequeue");
            await queue.CreateIfNotExistsAsync();
            //await queue.SendMessageAsync(source.Result);
        }
    }
}
