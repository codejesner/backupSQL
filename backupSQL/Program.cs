using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Timers;

namespace backupSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Ativo desde: {DateTime.Now}");
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 259200000;
            aTimer.Enabled = true;
            while (true) ;
        }

        static void backupSQL()
        {
            DateTime data = DateTime.Today;
            string diaDoBackup = data.ToString("dd.MM.yyyy");

            if (!Directory.Exists(@"C:\backups")) Directory.CreateDirectory(@"C:\backups");

            if (!File.Exists($@"C:\backups\backup_" + diaDoBackup + ".sql"))
            {
                string command = @"/C mysqldump -u root test > C:\backups\backup_" + diaDoBackup + ".sql";
                Process.Start("cmd.exe", command);
                Console.WriteLine($"O backup do dia {diaDoBackup} criado com sucesso!");
            }
            else
            {
                Console.WriteLine($"O backup do dia {diaDoBackup} já existe!");
            }
        }

        static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            backupSQL();
        }

    }

}
