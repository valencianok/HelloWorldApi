using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace HelloWorldApi.Global
{
    /// <summary>
    /// This is a wrapper for the configuration settings
    /// </summary>
    public class SolutionConfiguration
    {

        #region static

        /// <summary>
        /// Should be called from the startup to keep the global configuration var, and read the relevant properties
        /// </summary>
        /// <param name="configuration"></param>
        public static void Init(IConfiguration configuration)
        {
            Instance = new SolutionConfiguration();
            Instance.Configuration = configuration;
            configuration.GetSection("HWAPI").Bind(Instance);
        }

        public static SolutionConfiguration Instance { get; private set; }
        #endregion

        public IConfiguration Configuration { get; private set; }

        public SolutionConfiguration()
        {
        }

        public Uri DBEndpointURI { get; set; }

        public string DBAuthKey { get; set; }

        public string DBName { get; set; }


    }
}
