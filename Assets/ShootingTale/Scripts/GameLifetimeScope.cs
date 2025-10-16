using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private MenuView menuView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(menuView).As<IMenuView>();
        builder.Register<MenuModel>(Lifetime.Scoped);

        builder.RegisterEntryPoint<MenuPresenter>(Lifetime.Scoped)
               .As<IMenuPresenter>()
               .As<IMenuCommandHandler>();
    }
}
