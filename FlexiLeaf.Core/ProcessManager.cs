using System.Diagnostics;
using FlexiLeaf.Core.Network.Packets;

namespace FlexiLeaf.Core.ProcessManagement
{
    public class ProcessManager
    {
        public static List<ProcessPacket> GetRunningProcesses()
        {
            List<ProcessPacket> processList = new List<ProcessPacket>();

            try
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process process in processes)
                {
                    string name = process.ProcessName;
                    int PID = process.Id;
                    string cpuUsage = GetCpuUsage(process);
                    string ramUsage = GetRamUsage(process);
                    string networkUsage = GetNetworkUsage(process);

                    ProcessPacket processPacket = new ProcessPacket(name, PID, cpuUsage, ramUsage, networkUsage);
                    processList.Add(processPacket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la récupération des processus : " + ex.Message);
            }

            return processList;
        }

        public static void KillProcess(int processId)
        {
            try
            {
                Process process = Process.GetProcessById(processId);
                process.Kill();
                process.WaitForExit();
            }
            catch (ArgumentException)
            {
                // Le processus avec le PID spécifié n'existe pas
                // Gérer l'exception selon vos besoins
            }
            catch (Exception ex)
            {
                // Gérer toutes les autres exceptions possibles lors de la tentative de terminaison du processus
                // Gérer l'exception selon vos besoins
            }
        }


        private static string GetCpuUsage(Process process)
        {

            return "CPU usage";
        }

        private static string GetRamUsage(Process process)
        {

            return "RAM usage";
        }

        private static string GetNetworkUsage(Process process)
        {

            return "Network usage";
        }


    }
}
