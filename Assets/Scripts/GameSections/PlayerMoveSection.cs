using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerMoveSection : GameSection
{
    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = players.Any(p => p == other);
        if(isPlayer)
        {
            ClearSection();
        }
    }
}
