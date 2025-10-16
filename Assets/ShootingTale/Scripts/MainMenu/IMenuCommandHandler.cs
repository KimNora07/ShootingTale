using UnityEngine;

public interface IMenuCommandHandler
{
    void ExecuteStart();
    void ExecuteExit();
    void ExecuteSetting();
    void ExecuteOther();
    void ExecuteBack();
    void ExecuteVideo();
    void ExecuteAudio();
    void ExecuteHowTo();
    void ExecuteCredit();
    void ExecuteYes();
    void ExecuteNo();
}
