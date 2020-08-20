using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private static Player instance = null;



    public static Player Instance {
        get {
            if (instance == null) instance = new Player();
            return instance;
        }
    }
    Player()
    {

    }

    /// <summary>
    /// Daño del jugador
    /// </summary>
    int damage = 0;

    /// <summary>
    /// Referencia al daño
    /// </summary>
    public int Damage {
        get {
            return damage;
        }
        set {
            damage = value;
        }
    }
   
    /// <summary>
    /// Vida del jugador
    /// </summary>
    int health = 0;

    /// <summary>
    /// Getter y Setter de la vida
    /// </summary>
    public int Health {
        get {
            return health;
        }
        set {
            health = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
