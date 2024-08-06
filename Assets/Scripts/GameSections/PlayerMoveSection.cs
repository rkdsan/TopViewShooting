using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSection : GameSection
{
    public override void ActiveSection(PlayerController player)
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            ClearSection();
        }
    }
}
