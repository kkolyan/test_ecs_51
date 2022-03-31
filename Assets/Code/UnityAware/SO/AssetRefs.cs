using UnityEngine;

namespace UnityAware.SO
{
    /// <summary>
    /// Aggregator of all assets that are used from code.
    /// This class is used only to deliver these references to Zenject installers.
    /// Each particular asset exposed to the code using DI without passing AssetRefs everywhere.
    /// </summary>
    [CreateAssetMenu]
    public class AssetRefs: ScriptableObject
    {
        public GameObject pcPrefab;
        public SettingsAsset settingsAsset;
        public GameObject destPointerPrefab;
    }
}