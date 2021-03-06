﻿using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private PlayerManager playerManager;
    private CharacterStats myStats;

    void Start()
    {
        playerManager = PlayerManager.Instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.Player.GetComponent<CharacterCombat>();

        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}
