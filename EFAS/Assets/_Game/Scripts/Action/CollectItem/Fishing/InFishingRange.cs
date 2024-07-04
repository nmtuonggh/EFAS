using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFishingRange : MonoBehaviour
{
    [SerializeField] private BlackBoard _blackBoard;
    [SerializeField] private bool canFishing;

    public bool CanFishing
    {
        get => canFishing;
        set => canFishing = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FishingSpot"))
        {
            canFishing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FishingSpot"))
        {
            canFishing = false;
        }
    }
}
