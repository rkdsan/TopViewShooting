using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActiveSection : GameSection
{
    public override void ActiveSection(PlayerController player)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            ClearSection();
        }
    }

}
