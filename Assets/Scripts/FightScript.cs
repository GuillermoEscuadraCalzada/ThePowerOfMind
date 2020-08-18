using System.Collections;
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
    }
    #endregion

    GameManager gameManager;
    
    /// <summary>
    /// Aplica el daño actual que ambos personajes tengan
    /// </summary>
    public void ApplyDammage()
    {
        ///El daño y vida del jugador y enemigo es mayor a cero
        ///Alguno de los dos debe de tener un daño mayor a cero
        if( (gameManager.Player.Health > 0 && gameManager.Enemy.Health > 0))
        {
            Debug.Log("Applying Damage!");
            //Se le aplica el daño al enemigo
            if(gameManager.Player.Damage > 0 )
                gameManager.Enemy.Health -= gameManager.Player.Damage;
            //Se le aplica el daño al enemigo
            if (gameManager.Enemy.Damage > 0)
                gameManager.Player.Health -= gameManager.Enemy.Damage;
            gameManager.Enemy.Damage = 0;
            gameManager.Player.Damage = 0;
            gameManager.MemoramaManager.SetDamagesText();
        }
        else
        {
            Debug.Log("There is no health to lower!");
        }
    }
}
