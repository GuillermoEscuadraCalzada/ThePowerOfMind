using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Clase para los enemigos
/// </summary>
public class Enemy 
{
    private static Enemy instance = null;
    public static Enemy Instance {
        get {
            if (instance == null) instance = new Enemy();
            return instance;
        }
    }
    Enemy()
    {
        health = 0;
        damage = 0;
    }

    /// <summary>
    /// Vida del enemigo
    /// </summary>
    int health = 0;

    /// <summary>
    /// Referencia de la vida del jugador
    /// </summary>
    public int Health {
        get {
            return health;
        }
        set {
            health = value;
        }
    }
    
    int damage;

    /// <summary>
    /// Referencia del daño del enemigo
    /// </summary>
    public int Damage {
        get {
            return damage;
        }
        set {
            damage = value;
        }
    }

 
    // Update is called once per frame
    void Update()
    {
        
    }
}
