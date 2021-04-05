using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PopeyesModInstaller
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult SetAmongUsDir(Session session)
        {
#if DEBUG
            MessageBox.Show("Attach to rundll32.exe", "Attach");
#endif
            var dir = Directory.GetCurrentDirectory();

            session.Log("Begin SetAmongUsDir");

            File.WriteAllText("steam_appid.txt", "945360");

            var isSteamWorksInitialized = Steamworks.SteamAPI.Init();
            var isSteamRunning = Steamworks.SteamAPI.IsSteamRunning();
            session.Log("Is Steam Running: " + isSteamRunning);
            var amongUs = new Steamworks.AppId_t(945360);
            if (!isSteamRunning || !isSteamWorksInitialized)
            {
                session["AMONGUSDIR"] = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Among Us";
                return ActionResult.Success;
            }
            Steamworks.SteamApps.GetAppInstallDir(amongUs, out string amongUsDir, 260);
            
            session["AMONGUSDIR"] = amongUsDir;
            return ActionResult.Success;
        }
        [CustomAction]
        public static ActionResult CopyAmongUsDir(Session session)
        {
#if DEBUG
            MessageBox.Show("Attach to rundll32.exe", "Attach");
#endif
            session.Log("Copying Among Us directory");
            var installDir = session["APPDIR"];
            var amongUsDir = session["AMONGUSDIR"];

            List<string> copiedFiles = new List<string>();
            List<string> copiedDirectories = new List<string>();
            try
            {
                var files = Directory.GetFiles(amongUsDir);
                var directories = Directory.GetDirectories(amongUsDir);
                foreach (var file in files)
                {
                    var filePath = Path.Combine(installDir, Path.GetFileName(file));
                    if (!File.Exists(filePath))
                    {
                        File.Copy(file, filePath, false);
                        copiedFiles.Add(file);
                    }
                }
                foreach (var directory in directories)
                {
                    DirectoryCopy(directory, Path.Combine(installDir, Path.GetFileName(directory)), true);
                    copiedDirectories.Add(directory);
                }
                session.Log("Successfully copied Among Us Directory");
            }
            catch (Exception ex)
            {
                session.Log("Failed to copy Among Us directory: {0}", ex);
                foreach (var file in copiedFiles)
                {
                    File.Delete(file);
                }
                foreach (var directory in copiedDirectories)
                {
                    Directory.Delete(directory, true);
                }
            }
            return ActionResult.Failure;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                return;
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
