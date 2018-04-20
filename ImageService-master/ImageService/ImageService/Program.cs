using System.ServiceProcess;

namespace ImageService
{
    static class Program
    {
        /// <summary>main</summary>
        /// 
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ImageService(args)
            };
            ServiceBase.Run(ServicesToRun);
        }
      
        }
}
