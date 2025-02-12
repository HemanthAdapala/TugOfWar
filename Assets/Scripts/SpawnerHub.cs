using System;
using UnityEngine;

public class SpawnerHub : MonoBehaviour
{
    public void SpawnEntity(CardData cardData)
    {
        Debug.Log("Spawned Entity: " + cardData.cardName);
    }
}
