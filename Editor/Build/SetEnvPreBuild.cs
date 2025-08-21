using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Forgehub.Editor.Build
{
    public class SetEnvPreBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            // Read environment from CLI argument
            string envArg = GetCommandLineArg("buildEnv");
            if (string.IsNullOrEmpty(envArg))
            {
                Debug.LogWarning("⚠️ No buildEnv argument found. Using current EnvSO setting.");
                return;
            }
    
            var envManager = FindAssetByType<Enviroment.EnvManagerSO>();
            if (envManager == null)
            {
                Debug.LogError("❌ Could not find any EnvManagerSO asset in project.");
                return;
            }
            
            string envAssetName = $"Env_{UppercaseFirst(envArg)}";
            var envSO = FindAssetByName<Enviroment.EnvSO>(envAssetName);

            if (envSO == null)
            {
                Debug.LogError($"❌ EnvSO asset '{envAssetName}' not found in project.");
                return;
            }

            // Apply change
            envManager.EnvSO = envSO;
            EditorUtility.SetDirty(envManager);
            AssetDatabase.SaveAssets();

            Debug.Log($"✅ Build environment set to: {envArg}");
        }

        private string GetCommandLineArg(string name)
        {
            var args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith($"-{name}="))
                    return args[i].Substring(name.Length + 2);
            }
            return null;
        }

        private static string UppercaseFirst(string s) =>
            char.ToUpper(s[0]) + s.Substring(1);

        /// <summary>
        /// Finds the first asset of type T, skipping any whose name contains "Sample".
        /// </summary>
        private static T FindAssetByType<T>() where T : Object
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset != null && !asset.name.Contains("Sample"))
                    return asset;
            }
            return null;
        }

        /// <summary>
        /// Finds an asset in the project by its name and type.
        /// </summary>
        private static T FindAssetByName<T>(string assetName) where T : Object
        {
            string[] guids = AssetDatabase.FindAssets($"{assetName} t:{typeof(T).Name}");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset != null && asset.name == assetName)
                    return asset;
            }
            return null;
        }
    }
}
