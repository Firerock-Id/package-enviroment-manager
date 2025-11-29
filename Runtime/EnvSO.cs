using UnityEngine;

namespace Forgehub.Enviroment
{
    [CreateAssetMenu(fileName = "Env", menuName = "Scriptable Objects/EnvSO")]
    public class EnvSO : ScriptableObject
    {
        public EnvTypeEnum EnvTypeEnum;
        public string BaseURL;
        public bool EnableDebugConsole;
        public string EnvShortName => EnvTypeEnum switch
        {
            EnvTypeEnum.Development => "DEV",
            EnvTypeEnum.Staging => "STG",
            EnvTypeEnum.Production => "PRD",
            _ => "Unknown"
        };

        public string AccessToken;
        public string TokenType;
        public string Expiry;

    }
}