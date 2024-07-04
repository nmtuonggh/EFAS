using UnityEngine;

namespace _Game.Scripts.Action.Fishing
{
    public class OnFishing : MonoBehaviour
    {
        [SerializeField] private BlackBoard _blackBoard;
        [SerializeField] private InFishingRange _fishingRange;
        
        public void Fishing()
        {
            if (_fishingRange.CanFishing)
            {
                _blackBoard.isFishing = true;
            }
        }
    }
}