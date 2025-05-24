//UnityEngine

using UnityEngine;

public static class ConstantsManager
{
    public const string PlayerStr = "Player";
    public const string MasterParameter = "MasterVolume";

    private const string InvincibilityStr = "isInvincibility";

    private const string DieStr = "isDie";
    public static readonly int Invincibility = Animator.StringToHash(InvincibilityStr);
    public static readonly int Die = Animator.StringToHash(DieStr);
}