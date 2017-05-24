using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public static class Logs
    {
        public static string filePath = "";
        public static async Task AddLog(string action, string id, string title, string exMessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("----------");
            sb.Append("Error at: " + action);
            sb.Append("id: " + id);
            sb.Append("Title: " + title);
            sb.Append("Date: " + DateTime.Now);
            sb.Append("Message: " + exMessage);
            sb.Append("---------------------");
            // flush every 20 seconds as you do it
            File.AppendAllText(filePath + "log.txt", sb.ToString());
            sb.Clear();
            await Task.Run(() => { Thread.Sleep(1); });
        }
    }
}
