namespace Nekit
{
    public class Config
    {
        public int Frequency;
        public string DayTime;
        public string NightTime;
        public string DayFolderPath;
        public string NightFolderPath;
        public string NormalFolderPath;
        public string[] ExtensionsList;
        public States State;
        public enum States
        {
            DayNight = 1,
            Normal = 2
        }
    }
}
