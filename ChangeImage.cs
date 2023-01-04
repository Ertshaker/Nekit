using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace Nekit
{
    public partial class ChangeImage
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(
            Int32 uAction,
            Int32 uParam,
            String lpvParam,
            Int32 fuWinIni
            );
        const Int32 SPI_SETDESKWALLPAPER = 0x14;
        const Int32 SPIF_UPDATEINIFILE = 0x01;
        const Int32 SPIF_SENDWININICHANGE = 0x02;

        readonly string userRoot = "HKEY_CURRENT_USER\\Control Panel\\Desktop";

        List<string> list = new List<string>();
        Timer timer;

        int frequency;
        int dayTime;
        int nightTime;
        string dayFolderPath;
        string nightFolderPath;
        string normalFolderPath;
        TimeState timeState, previousTimeState;
        enum TimeState { Day, Night }
        int i = 0;

        public ChangeImage(int frequency, string dayFolderPath, string nightFolderPath, string dayTime, string nightTime)
        {
            this.frequency = frequency;
            this.dayTime = Convert.ToInt32(dayTime.Replace(":", ""));
            this.nightTime = Convert.ToInt32(nightTime.Replace(":", ""));
            this.dayFolderPath = dayFolderPath;
            this.nightFolderPath = nightFolderPath;
        }

        public ChangeImage(int frequency, string normalFolderPath)
        {
            this.frequency = frequency;
            this.normalFolderPath = normalFolderPath;
        }

        public void Start() => timer = new Timer(Change, null, 0, frequency);

        void checkTime()
        {
            int currentTime = Convert.ToInt32(DateTime.Now.ToString("H:mm").Replace(":", ""));

            if (currentTime > nightTime || currentTime < dayTime)
                timeState = TimeState.Night;

            else if (currentTime > dayTime && currentTime < nightTime)
                timeState = TimeState.Day;
        }

        void getFiles()
        {
            if (MainWindow.ConfigList[0].State is Config.States.Normal)
            {
                foreach (string file in Directory.GetFiles(normalFolderPath))
                    if (MainWindow.ConfigList[0].ExtensionsList.Any(x => x == Path.GetExtension(file)))
                        list.Add(file);
            }

            else if (timeState == TimeState.Day)
            {
                foreach (string file in Directory.GetFiles(dayFolderPath))
                    if (MainWindow.ConfigList[0].ExtensionsList.Any(x => x == Path.GetExtension(file)))
                        list.Add(file);
            }

            else if (timeState == TimeState.Night)
            {
                foreach (string file in Directory.GetFiles(nightFolderPath))
                    if (MainWindow.ConfigList [0].ExtensionsList.Any(x => x == Path.GetExtension(file)))
                        list.Add(file);
            }
        }

        void Change(Object? stateinfo)
        {

            if (MainWindow.ConfigList[0].State == Config.States.DayNight)
                checkTime();

            if (previousTimeState != timeState || list.Count == 0)
                getFiles();

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, list[i++], SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
;
            if (i == list.Count)
                i = 0;

            previousTimeState = timeState;
        }

        public void Stop() => timer?.Dispose();
    }
}
