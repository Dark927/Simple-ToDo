using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    public static class Watchdog
    {
        public static void StartWatchdog()
        {
            // Получаем путь к текущему приложению
            string currentAppPath = Process.GetCurrentProcess().MainModule.FileName;

            // Создаем процесс watchdog
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = currentAppPath,
                Arguments = "watchdog", // Передаем аргумент для определения режима работы
                CreateNoWindow = true,   // Запуск без окна
                WindowStyle = ProcessWindowStyle.Hidden, // Окно будет скрыто
                UseShellExecute = false
            };

            Process.Start(startInfo);
        }

        private static void StartMainProcess()
        {
            string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;

            Process.Start(new ProcessStartInfo
            {
                FileName = mainAppPath,
                UseShellExecute = false
            });
        }

        public static void RunWatchdog()
        {
            // Получаем основной процесс (текущий экземпляр)
            Process mainProcess = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)
                                         .FirstOrDefault(p => p.Id != Process.GetCurrentProcess().Id);

            // Если основной процесс не найден, запускаем его
            if (mainProcess == null)
            {
                StartMainProcess();
                return;
            }

            // Следим за состоянием основного процесса
            mainProcess.EnableRaisingEvents = true;
            mainProcess.Exited += (sender, args) =>
            {
                // Перезапускаем основной процесс, если он завершился
                StartMainProcess();
            };

            // Ожидание завершения основного процесса
            mainProcess.WaitForExit();
        }
    }
}
