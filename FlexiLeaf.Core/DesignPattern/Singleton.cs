using System.Reflection;

namespace Giny.Core.DesignPattern
{
    public interface ISingleton<T> where T : new()
    {
        static T Instance { get; }
        
        private static T _instance;
    }

    public class Singleton<T>  where T : new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }


}
