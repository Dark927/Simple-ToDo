using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using IWshRuntimeLibrary;

namespace ToDoApp
{
    internal class SaveManager
    {
        public static void AddToStartupFolder()
        {
            // Получаем путь к .exe файлу текущего процесса
            string appPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            // Путь к папке автозагрузки
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolderPath, "MyWpfApp.lnk");

            // Создаем объект WshShell для создания ярлыка
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

            // Настраиваем параметры ярлыка
            shortcut.Description = "Sosal"; // Описание ярлыка
            shortcut.TargetPath = appPath; // Путь к исполняемому файлу приложения
            shortcut.Save(); // Сохраняем ярлык
        }
    }
}

