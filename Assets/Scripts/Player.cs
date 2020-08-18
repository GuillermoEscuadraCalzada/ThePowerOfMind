using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static Player Instance {
        get {
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
