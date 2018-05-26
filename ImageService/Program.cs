using System.ServiceProcess;

namespace ImageService
{
    static class Program
    {
        /// <summary>
        /// main
        /// </summary>
        static void Main(string[] args)
        {
            //The services to run
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                //Creates a new image service object
                new ImageService(args)
            };
            //runs services
            ServiceBase.Run(ServicesToRun);
        }
    }
}
