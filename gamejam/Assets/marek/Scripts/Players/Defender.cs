using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Player
{
    private void Awake()
    {
        SetUp();
        Username = PlayerNames.DefenderUsername;
        SetPlayerText();
        PlayerRole = PlayerType.Defender;
    }
}
