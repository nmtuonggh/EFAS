using UnityEngine;

namespace _Game.Scripts.Action.PickFruit
{
    public class InPickFruitRange : MonoBehaviour
    {
        [SerializeField] private BlackBoard _blackBoard;
        [SerializeField] private bool pickFruit;
        [SerializeField] private Transform targetTree;

        public bool PickFruit
        {
            get => pickFruit;
            set => pickFruit = value;
        }

        public Transform TargetTree
        {
            get => targetTree;
            set => targetTree = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CollectTree"))
            {
                pickFruit = true;
                targetTree = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CollectTree"))
            {
                pickFruit = false;
            }
        }
    }
}