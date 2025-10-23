using System.Collections;
using UnityEngine;

public class PlayerPresenter
{
    private readonly IPlayerView view;
    private readonly PlayerModel model;

    public PlayerPresenter(IPlayerView view, PlayerModel model)
    {
        this.view = view;
        this.model = model;

        model.OnHealthChanged += OnHealthChanged;
        Initialize();
    }

    public void Initialize()
    {
        view.ResetLinePosition();
        view.SetActive(false);
    }

    private void OnHealthChanged(int currentHealth)
    {
        view.UpdateHealthText(currentHealth);
    }

    public void SelectedAttackSign()
    {
        view.SetActive(true);
        view.LineMove(null, () =>
        {
            float x = view.GetLinePosition().x;
            model.currentAtk = GetAttackBasedOnX(x);
            view.SetActive(false);
        }, null);
    }

    private int GetAttackBasedOnX(float x)
    {
        return x switch
        {
            < -68.5f => RandomAttack(model.PlayerLowAtk, 0),
            < -36.5f => RandomAttack(model.PlayerLowAtk, 2),
            < -13f => RandomAttack(model.PlayerMediumAtk, 0),
            < -1.5f => RandomAttack(model.PlayerMediumAtk, 2),
            < 1.5f => RandomAttack(model.PlayerHighAtk, 0),
            < 13f => RandomAttack(model.PlayerMediumAtk, 2),
            < 36.5f => RandomAttack(model.PlayerMediumAtk, 0),
            < 68.5f => RandomAttack(model.PlayerLowAtk, 2),
            var _ => RandomAttack(model.PlayerLowAtk, 0)
        };
    }
    
    private static int RandomAttack(int[] atkArray, int start)
    {
        int index = Random.Range(start, atkArray.Length);
        return atkArray[index];
    }
}
