using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Core
{
    public class CommandExecutor
    {
        private Process cmdProcess;
        private StreamWriter cmdStreamWriter;
        private string cmdExecution;

        public CommandExecutor()
        {
            cmdExecution = Path.Combine(Environment.SystemDirectory, "cmd.exe");

            cmdProcess = new Process();
            cmdProcess.StartInfo.FileName = cmdExecution;
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            cmdProcess.StartInfo.RedirectStandardInput = true;
            cmdProcess.StartInfo.CreateNoWindow = true;
            cmdProcess.Start();

            cmdStreamWriter = cmdProcess.StandardInput;
        }

        public string ExecuteCommand(string command)
        {
            cmdStreamWriter.WriteLine(command + " 2>&1");
            cmdStreamWriter.WriteLine("echo end");
            cmdStreamWriter.Flush();

            string line = "";
            List<string> resultLines = new List<string>();
            do
            {
                line = cmdProcess.StandardOutput.ReadLine();
                if (line == null || line == "end") break;
                line = line.Replace(" 2>&1", "");
                resultLines.Add(line);
            } while (true);

            if (resultLines.Count >= 2) // remove the last two lines
            {
                resultLines.RemoveAt(resultLines.Count - 1); // remove 'echo end' line
                //resultLines.RemoveAt(resultLines.Count - 1); // remove 'command' line
            }

            return string.Join(Environment.NewLine, resultLines);
        }
    }
}
