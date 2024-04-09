using System.Reflection;

namespace Server
{
    internal class ImageList
    {
        private IDictionary<string, string> _imageList;
        private string _imageDirectory;

        public ImageList() 
        {
            _imageList = new Dictionary<string, string>();
            _imageDirectory = "";
            
        }

        public IDictionary<string, string> getImageList()
        {
            return _imageList;
        }

        public string GetCurrentDirectory()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyPath = assembly.Location;
            string currentDirectory = Path.GetDirectoryName(assemblyPath);


            for (int i = 0; i < 3; i++)
            {
                currentDirectory = Path.GetDirectoryName(currentDirectory);
                if (currentDirectory == null)
                {
                    Console.WriteLine("Cannot go up any further!");
                    break;
                }
            }

            return currentDirectory;
        }

        public void CreateImageList()
        {
            string currentDirectory = GetCurrentDirectory();
            _imageDirectory = currentDirectory + @"\Server Images";
            string[] imagePaths = Directory.GetFiles(_imageDirectory);
            foreach (string imagePath in imagePaths) 
            {
                string imageName = Path.GetFileName(imagePath);
                _imageList.Add(imageName, imagePath);
            }

        }

        public bool ContainsKey(string key)
        {
            return _imageList.ContainsKey(key);
        } 

        public string GetImagePath(string key) 
        {
            return _imageList[key];
        }
        
        public string GetImageDirectory()
        {
            return _imageDirectory;
        }
        
    }
}
