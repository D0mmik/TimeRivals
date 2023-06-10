using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Player
{
    private void Awake()
    {
        Username = PlayerNames.AttackerUsername;
        SetPlayerText();
        PlayerRole = PlayerType.Attacker;
    }
}
