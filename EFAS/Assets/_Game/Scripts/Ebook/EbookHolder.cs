using UnityEngine;

namespace _Game.Scripts.Ebook
{
    public class EbookHolder : MonoBehaviour
    {
        [SerializeField] private int _eBookSize;
        public EbookSystem ebookSystem;

        public EbookSystem EbookSystem
        {
            get => ebookSystem;
            set => ebookSystem = value;
        }

        private void Awake()
        {
            EbookSystem = new EbookSystem(_eBookSize);
        }
    }
}