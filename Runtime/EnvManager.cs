using UnityEngine;

namespace Forgehub.Enviroment
{
    public class EnvManager : Singleton<EnvManager>
    {
        [SerializeField] private EnvManagerSO envManagerSO;

        public string BaseURL { get => envManagerSO.EnvSO.BaseURL; }
        public bool EnableDebugConsole { get => envManagerSO.EnvSO.EnableDebugConsole; }

        public EnvSO CurrEnvSO => envManagerSO.EnvSO;

        public bool IsProduction()
        {
            return envManagerSO.EnvSO.EnvTypeEnum == EnvTypeEnum.Production;
        }

        public bool IsStaging()
        {
            return envManagerSO.EnvSO.EnvTypeEnum == EnvTypeEnum.Staging;
        }

        public bool IsDevelopment()
        {
            return envManagerSO.EnvSO.EnvTypeEnum == EnvTypeEnum.Development;
        }
    }

}
