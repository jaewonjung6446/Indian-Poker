using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMin : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        NPCAI.Instace.burstedCoin = other.GetComponent<Collider>();
        NPCAI.Instace.GetMinDistanceNPC();
    }
}
