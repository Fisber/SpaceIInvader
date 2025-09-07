using Controllers;
using SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EntryPoints
{
    public class SceneScope : LifetimeScope
    {
        [SerializeField] private ObjectPool Pool;
        [SerializeField] private GunFireController GunFireController;
        [SerializeField] private GunMoverController GunMoverController;
        [SerializeField] private EnemyRowsManager EnemyRowsManager;
        [SerializeField] private DataHolder DataHolder;
        [SerializeField] private UiController UiController;
        [SerializeField] private LevelHolder Level;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(Pool);
            builder.RegisterComponent(GunFireController);
            builder.RegisterComponent(EnemyRowsManager);
            builder.RegisterComponent(DataHolder.UserData);
            builder.RegisterComponent(Level.LevelIds);
            builder.RegisterComponent(UiController);
            builder.RegisterComponent(GunMoverController);
            
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<GameLoop>();
            });
        }
    }
}