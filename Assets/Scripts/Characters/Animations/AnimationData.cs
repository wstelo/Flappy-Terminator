using UnityEngine;

public class AnimationData : MonoBehaviour
{
    public static readonly int IsFly = Animator.StringToHash(nameof(IsFly));
    public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
}
