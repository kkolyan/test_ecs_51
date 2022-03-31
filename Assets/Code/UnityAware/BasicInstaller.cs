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
        Container.BindInstance(new EcsWorld()).WithId("short");
        Container.Bind<AssetRefs>().FromScriptableObject(assetRefs).AsTransient();
        Container.Bind<Pc.Factory>().AsSingle();
        Container.Bind<DestPointer.Factory>().AsSingle();
        Container.Bind<Pc>().FromComponentInNewPrefab(assetRefs.pcPrefab).AsTransient();
        Container.Bind<DestPointer>().FromComponentInNewPrefab(assetRefs.destPointerPrefab).AsTransient();
        Container.Bind<NavMeshAgent>().FromComponentSibling().AsTransient();
        Container.BindInstance(assetRefs.settingsAsset.gameSettings);
    }
}