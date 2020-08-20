using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class EnemyGO : MonoBehaviour
{
    #region Instances
    private static EnemyGO instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    
        }
        else
        {
            Destroy(this);
        }
    }
    public static EnemyGO Instance {
        get {
            
            return instance;
        }
    }
    public Enemy enemyInstance;
    #endregion

    #region TextMesh
    [SerializeField] TextMeshProUGUI enemyHealth;
    [SerializeField] TextMeshProUGUI enemyDamage;

    public TextMeshProUGUI EnemyDamage {
        get {
            return enemyDamage;
        }
    }

    public TextMeshProUGUI EnemyHealth {
        get {
            return enemyHealth;
        }
    }
    #endregion


    /// <summary>
    /// Lista de sprites del enemigo
    /// </summary>
    List<Sprite> enemySprites;

    /// <summary>
    /// Lista temporal de los sprites para reusar imágenes
    /// </summary>
    List<Sprite> temporalSprites;
  
    System.Random rng;
    [SerializeField] Image img;

    // Start is called before the first frame update
    void Start()
    {
        rng = new System.Random();
        enemyInstance = Enemy.Instance;
        LoadEnemyImages();
        SetCurrentImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Carga los sprites de los enemigos
    /// </summary>
    void LoadEnemyImages()
    {
        enemySprites = new List<Sprite>(); ///Se crea una nueva lista 
        Sprite[] spritesArr = Resources.LoadAll<Sprite>("Enemies"); ///Se cargan todos los sprites de la carpeta Enemies
        if (spritesArr != null) ///Si es diferente de nulo, es que sí existe
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
        if (temporalSprites == null || temporalSprites.Count <= 0)
            temporalSprites = new List<Sprite>(enemySprites);
        if (img == null) ///Si es nula agregale el componente
        {
            gameObject.AddComponent<Image>();
        }
        int index = 0;
        index = rng.Next(0, temporalSprites.Count); ///Índice aleatoria de los elementos en la lista
        img.sprite = temporalSprites[index]; ///Actualiza el sprite de la imagen
        temporalSprites.RemoveAt(index); ///Quita la imagen del enemigo de la lista

    }
    
    public void UpdateHealthText()
    {
        enemyHealth.text = "Health: " + enemyInstance.Health.ToString();
    }

}
