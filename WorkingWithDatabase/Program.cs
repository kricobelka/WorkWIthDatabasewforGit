using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;

namespace WorkingWithDatabase
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var entityFrameworkExample = new EntityFrameWorkExample();
            await entityFrameworkExample.MakeEf();
        }

    }
}
