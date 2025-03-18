using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.AdvertisingServices;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Inputs;
using _Project.Scripts.Services.LoadSystem;
using _Project.Scripts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.Services.MoveItems;
using _Project.Scripts.Services.PauseSystem;
using _Project.Scripts.Services.SaveSystem;
using _Project.Scripts.ShopSystem;
using Zenject;

namespace _Project.Scripts.GameStateMachine.Context
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            RegisterAnalyticService(Container);
            RegisterAdsServices(Container);
            RegisterPurchaseServices(Container);
            RegisterServices(Container);
        }
        

        private void RegisterAnalyticService(DiContainer container)
        {
            container.Bind<IAnalytics>().To<EventHandler>().AsSingle();
        }

        private void RegisterAdsServices(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<UnityAdsInitializer>().AsSingle();
            container.Bind<IShowReward>().To<RewardHandler>().AsCached();
            container.Bind<IAdvertisingShow>().To<AdvertisingHandler>().AsSingle();
        }

        private void RegisterPurchaseServices(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<GameStoreListener>().AsSingle();
            container.BindInterfacesAndSelfTo<GameStoreInitialize>().AsSingle();
        }

        private void RegisterServices(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
            container.Bind<PlayerObjectCollector>().AsSingle();
            container.Bind<SwitchingService>().AsSingle();
            container.Bind<SaveDataHandler>().AsSingle();
            container.Bind<PauseService>().AsSingle();
            container.BindInterfacesAndSelfTo<MovementPlayer>().AsSingle().NonLazy();
            container.Bind<IConfigHandler>().To<ConfigHandler>().AsSingle();
            container.Bind<ISaveStrategy>().To<SaveToFile>().AsSingle();
            container.Bind<SceneLoader>().AsSingle();
            container.Bind<EntityLoaderService>().AsSingle();
        }
    }
}