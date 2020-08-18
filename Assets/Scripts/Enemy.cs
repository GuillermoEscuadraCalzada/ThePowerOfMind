using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Clase para los enemigos
/// </summary>
public class Enemy : MonoBehaviour
{
    private static Enemy instance = null;
    public static Enemy Instance {
        get {
            return instance;
        }
    }
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

    /// <summary>
    /// Vida del enemigo
    /// </summary>
    [Header("Vida del enemigo")]
    [SerializeField] int health = 0;

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

    /// <summary>
    /// Daño del enemigo
    /// </summary>
    [Header("Daño del enemigo")]
    [SerializeField] int damage = 0;

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

    /// <summary>
    /// Lista de sprites del enemigo
    /// </summary>
    List<Sprite> enemySprites;

    /// <summary>
    /// Lista temporal de los sprites para reusar imágenes
    /// </summary>
    List<Sprite> temporalSprites;
    System.Random rng;
    Image img;
    Enemy()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        rng = new System.Random();
        LoadEnemyImages();
        SetCurrentImage();
    }

    /// <summary>
    /// Carga los sprites de los enemigos
    /// </summary>
    void LoadEnemyImages()
    {
        enemySprites = new List<Sprite>(); ///Se crea una nueva lista 
        Sprite[] spritesArr = Resources.LoadAll<Sprite>("Enemies"); ///Se cargan todos los sprites de la carpeta Enemies
        if(spritesArr != null) ///Si es diferente de nulo, es que sí existe
        {
            ///Por cada uno de los sprites
            foreach (Sprite sprite in spritesArr)
            {
                enemySprites.Add(sprite); ///Añádelos a la lista
            }
        }
    }
   
    /// <summary>
    /// Establece la nueva imagen del enemigo
    /// </summary>
    void SetCurrentImage()
    {
        ///La lista es nula o su contenido es mayor o igual a cero
        if(temporalSprites == null || temporalSprites.Count <= 0)
            temporalSprites = new List<Sprite>(enemySprites);
        img = GetComponent<Image>(); ///Obten el componente de la imagen
        if(img == null) ///Si es nula agregale el componente

        {
            gameObject.AddComponent<Image>();
        }
        int index = 0;
        index = rng.Next(0, temporalSprites.Count); ///Índice aleatoria de los elementos en la lista
        img.sprite = temporalSprites[index]; ///Actualiza el sprite de la imagen
        temporalSprites.RemoveAt(index); ///Quita la imagen del enemigo de la lista

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
