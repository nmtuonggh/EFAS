using System;
using Animancer;
using UnityEngine;

namespace _Game.Scripts.Inventory.Action
{
    public class PreviewAnim : MonoBehaviour
    {
        public AnimancerComponent animancer;
        public LinearMixerTransition _carryAnim;
        public ClipTransition _idle;
        public BlackBoard blackBoard;

        private void Update()
        {
            if (blackBoard.PreviewHolder.ItemCount != 0)
            {
                ((LinearMixerState)animancer.Play(_carryAnim)).Parameter = Mathf.Lerp(((LinearMixerState)animancer.Play(_carryAnim)).Parameter, blackBoard.PreviewHolder.ItemCount, 55 * Time.deltaTime);
            }
            else
            {
                animancer.Play(_idle);
            }
        }
    }
}