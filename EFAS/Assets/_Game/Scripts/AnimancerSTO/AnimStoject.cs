using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
[CreateAssetMenu(fileName = "AnimStoject", menuName = "AnimancerSTO/AnimStoject")]
public class AnimStoject : ScriptableObject
{
    [SerializeField] private AnimancerComponent _animancerComponent;
    [SerializeField] protected AnimationClip _animIdle;
    [SerializeField] private AnimationClip _animRun;
    [SerializeField] private AnimationClip _animWalk;
    [SerializeField] private AnimationClip _animJump;
    [SerializeField] private AnimationClip _animFall;
    [SerializeField] private AnimationClip _animLand;
}
