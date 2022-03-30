using GameCore;
using Leopotam.EcsLite;
using UnityAware.MonoBehs;
using UnityAware.SO;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[CreateAssetMenu(fileName = "BasicInstaller", menuName = "Installers/BasicInstaller")]
public class BasicInstaller : ScriptableObjectInstaller<BasicInstaller>
{
    public AssetRefs assetRefs;
    
    public override void InstallBindings()
    {
        Container.BindInstance(new CoreTime());
        Container.BindInstance(new EcsWorld());
        Container.Bind<AssetRefs>().FromScriptableObject(assetRefs).AsTransient();
        Container.Bind<PcDoll.Factory>().AsSingle();
        Container.Bind<DestMarker.Factory>().AsSingle();
        Container.Bind<PcDoll>().FromComponentInNewPrefab(assetRefs.pcPrefab).AsTransient();
        Container.Bind<DestMarker>().FromComponentInNewPrefab(assetRefs.destMarkerPrefab).AsTransient();
        Container.Bind<NavMeshAgent>().FromComponentSibling().AsTransient();
        Container.BindInstance(assetRefs.settingsAsset.gameSettings);
    }
}