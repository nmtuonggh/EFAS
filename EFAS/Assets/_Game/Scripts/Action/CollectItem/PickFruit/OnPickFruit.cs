using UnityEngine;

namespace _Game.Scripts.Action.PickFruit
{
    public class OnPickFruit : MonoBehaviour
    {
        [SerializeField] private BlackBoard _blackBoard;
        [SerializeField] private InPickFruitRange _pickFruitRange;
        
        public void Picking()
        {
            if (_pickFruitRange.PickFruit)
            {
                _blackBoard.isPicking = true;
            }
        }
    }
}