using System.IO;

namespace Logic.Manager
{
    public class ResManager
    {
        private ResManager()
        {
        
        }
    
        public void Init()
        {
            UIConfigResMapMsg = UIConfigResMapMsg.Parser.ParseFrom(LoadByte("UIConfig"));
        }

        private FileStream LoadByte(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"../../Resource/Data/{fileName}.byte");
            return new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite); 
        }

        public UIConfigResMapMsg UIConfigResMapMsg;
        private static ResManager _instance;
        public static ResManager Instance => _instance ??= new ResManager();
    }
}
