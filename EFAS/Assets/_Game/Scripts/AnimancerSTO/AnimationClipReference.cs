using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationClipReference", menuName = "Animation/Animation Clip Reference")]
public class AnimationClipReference : ScriptableObject
{
    public AnimationClip idleClip;
    public AnimationClip walkClip;
}
