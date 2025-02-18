﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScript 
{
    #region Instance
    private static FightScript instance = null;

    public static FightScript Instance {
        get {
            if (instance == null) instance = new FightScript();
            return instance;
        }
    }

    FightScript()
    {
        gameManager = GameManager.Instance;
        sceneManager = SceneManager.Instance;
        enemy = EnemyGO.Instance;
        player = PlayerGO.Instance;
    }
    #endregion

    GameManager gameManager;
    SceneManager sceneManager;
    EnemyGO enemy;
    PlayerGO player;
    /// <summary>
    /// Aplica el daño actual que ambos personajes tengan
    /// </summary>
    public void ApplyDammage()
    {
        if(MemoramaManager.Instance.CardsSwaped.Count == MemoramaManager.Instance.CardLimit - 1)
        {
            Debug.LogError("Can't apply Damage!");
            return;
        }
        ///El daño y vida del jugador y enemigo es mayor a cero
        ///Alguno de los dos debe de tener un daño mayor a cero
        if( (player.playerInstance.Health > 0 && enemy.enemyInstance.Health > 0))
        {
            Debug.Log("Applying Damage!");

            //Se le aplica el daño al enemigo
            if(player.playerInstance.Damage > 0 ) enemy.enemyInstance.Health -= player.playerInstance.Damage;
            //Se le aplica el daño al enemigo
            if (enemy.enemyInstance.Damage > 0) player.playerInstance.Health -= enemy.enemyInstance.Damage;

            if (player.playerInstance.Health <= 0)
            {
                GameManager.Instance.Restart();
                sceneManager.ShowScene(SceneTagGO.DEFEATSCENE);
            }
            else if (enemy.enemyInstance.Health <= 0)
            {
                GameManager.Instance.Restart();
                EnemyGO.Instance.SetCurrentImage();
                
                sceneManager.ShowScene(SceneTagGO.VICTORYSCENE);
            }
            gameManager.SetHealthAndDamage();
        }
        else
        {
            Debug.Log("There is no health to lower!");
        }
    }
}
