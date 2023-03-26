namespace FolderSync.LoggerC
{
    using System;
    using System.IO;

    public class Logger
    {
        private readonly string logFilePath;
        public Logger(string logFilePath)
        {
            this.logFilePath = logFilePath;

            try
            {
                if (!File.Exists(logFilePath))
                    File.Create(logFilePath).Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating log file at {logFilePath}: {ex.Message}");
            }
        }

        public void Log(string message)
        {
            try
            {
                using (var streamWriter = File.AppendText(logFilePath))
                {
                    streamWriter.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file at {logFilePath}: {ex.Message}");
            }
        }
    }
}
