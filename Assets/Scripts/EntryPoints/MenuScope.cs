using Controllers;
using SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EntryPoints
{
    public class MenuScope : LifetimeScope
    {
        [SerializeField] private DataHolder Data;
        [SerializeField] private UiMenuController UiMenuController;
        [SerializeField] private LevelHolder Level;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(Data.UserData);
            builder.RegisterComponent(UiMenuController);
            builder.RegisterComponent(Level.LevelIds);
            
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<MenuLoop>();
            });
        }

    }
}