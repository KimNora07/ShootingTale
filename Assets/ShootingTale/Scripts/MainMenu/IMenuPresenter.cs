using System;
using VContainer.Unity;

public interface IMenuPresenter : IStartable, IDisposable
{
    void Initialize();
    void ConfirmFromBar();
}
