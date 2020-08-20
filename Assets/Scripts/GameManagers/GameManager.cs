using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase encargada de distribuir elementos para la funcionalidad del juego
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Instances
    private static GameManager instance = null;
    public static GameManager Instance {
        get {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
   
    /// <summary>
    /// Referencia a Memorama Manager
    /// </summary>
    MemoramaManager memoramaManager;
    public MemoramaManager MemoramaManager {
        get {
            return memoramaManager;
        }
    }

    /// <summary>
    /// Referencia al jugador
    /// </summary>
    PlayerGO player;
    /// <summary>
    /// Getter y setter del jugador
    /// </summary>
    public PlayerGO Player {
        get {
            return player;
        }
    }

    /// <summary>
    /// Referencia al enemigo
    /// </summary>
    EnemyGO enemy;

    /// <summary>
    /// Getter y setter del enemigo
    /// </summary>
    public EnemyGO Enemy {
        get {
            return enemy;
        }
    }

    FightScript fightScript;
    public FightScript FightScript {
        get {
            return fightScript;
        }
    }
   
    RenewBoard renewBoard;
    public RenewBoard RenewBoard {
        get {
            return renewBoard;
        }
    }
    #endregion

    #region contenedores
    // Start is called before the first frame update
    [SerializeField] List<GameObject> cardsParentsList;
    public List<GameObject> CardsParentsList {
        get {
            return cardsParentsList;
        }
    }
    /// <summary>
    /// Lista de sprites
    /// </summary>
    List<SpriteScript> spritesScriptList;

    /// <summary>
    /// Arreglo de daños
    /// </summary>
    int[] damageArray = new int[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
    #endregion

    [SerializeField] private int playersHealth = 50;
    System.Random rng;

    #region botones
    [Header("Buttons")]
    public Button fightButton;
    public Button renewButton;
    #endregion


    enum GameStatus
    {
        PLAYING,
        DEFEATED,
        WON,
        NIL 
    }
    GameStatus gameStatus = GameStatus.NIL;
    void Start()
    {
        rng = new System.Random();
        LoadSpriteResources();
        GetCardsParent();
        AddButtonsToChildren();
        GetInstances();
        SetRandomSprites();
    }

    /// <summary>
    /// Se obtienen todas las instancias del juego
    /// </summary>
    private void GetInstances()
    {
        memoramaManager = MemoramaManager.Instance;
        fightScript = FightScript.Instance;
        renewBoard = RenewBoard.Instance;
        player = PlayerGO.Instance;
        enemy = EnemyGO.Instance;
        SetHealthAndDamage();
        renewBoard.GetBoardChildren();
        fightButton.onClick.AddListener(fightScript.ApplyDammage);
        renewButton.onClick.AddListener(renewBoard.CheckIfBoardCanBeRenewed);

    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.Instance.CurrentScene == SceneTagGO.GAMESCENE)
        {
            memoramaManager.CheckCurrentCards();
            renewBoard.Update();
        }
    }


    /// <summary>
    /// Se establecen la vida del jugador y del enemigo
    /// </summary>
    public void SetHealthAndDamage()
    {
        string health = "Health: ", damage = "Damage: ";
        if((enemy.enemyInstance.Health <= 0|| player.playerInstance.Health <= 0)) enemy.enemyInstance.Health = player.playerInstance.Health = playersHealth; ///Jugador y enemigo empiezan con 50 de vida
        enemy.enemyInstance.Damage = player.playerInstance.Damage = 0;

        ///Se establecen los textos del enemigo
        enemy.EnemyDamage.text = damage + enemy.enemyInstance.Damage.ToString();
        enemy.EnemyHealth.text = health + enemy.enemyInstance.Health.ToString();

        ///Se establecen los textos del jugador
        player.PlayerHealth.text = health + player.playerInstance.Health.ToString();
        player.PlayerDamage.text = damage + player.playerInstance.Damage.ToString();

    }

    /// <summary>
    /// Se obtiene el padre de todos los renglones de cartas
    /// </summary>
    void GetCardsParent()
    {
        cardsParentsList = new List<GameObject>(); //Crea una nueva lista de objets

        ///Crea un nuevo arreglo de objetos de toda la escena
        GameObject[] objectArr = Resources.FindObjectsOfTypeAll<GameObject>() as GameObject[];
        
        ///Para cada uno de los objetos
        foreach (GameObject gameObject in objectArr)
        {
            CardParent cParent = gameObject.GetComponent<CardParent>(); ///Obren este componente
                                                                        ///Si no es nulo
            if (cParent != null)
            {
                cardsParentsList.Add(gameObject); ///Añádelo a la lista
            }
        }
    }

    /// <summary>
    /// Se añaden botones a cada uno de los hijos de los padres
    /// </summary>
    void AddButtonsToChildren()
    {
        GameObject obj; ///Guarda un objeto referencia que funcionará más adelante
        CardScript cardScript; ///Guarda una variable de este script

        ///Itera por la list de padres
        for (int i = 0; i < cardsParentsList.Count; i++)
        {
            ///Itera por cada uno de los hijos de este padre i
            for (int child = 0; child < cardsParentsList[i].transform.childCount; child++)
            {
                ///Obten una referencia al GO de este hijo
                obj = cardsParentsList[i].transform.GetChild(child).gameObject;
                if (obj != null)
                {///Si el objeto es diferente de nulo
                    obj.AddComponent<Button>(); ///Añadele un botón
                    cardScript = obj.AddComponent<CardScript>(); ///Crea un script y añadelo al objeto
                    obj.GetComponent<Button>().onClick.AddListener(cardScript.FadeOutAction); ///Añade este evento al botón
                }
            }
        }
    }

    /// <summary>
    /// Carga todos los sprites importantes para el juego de manera dinámica
    /// </summary>
    void LoadSpriteResources()
    {
        spritesScriptList = new List<SpriteScript>(); ///Crea una nueva lista de sprites
                                                      ///Busca todos los componentes en la carpeta Resources/Sprites y los guarda en un arreglo
        Sprite[] spritesArr = Resources.LoadAll<Sprite>("Sprites");
        List<int> dmg = new List<int>(damageArray);
        if (spritesArr != null)
        {
            ///Itera por el arreglo y añádelo a la lista de sprites
            foreach (Sprite sprite in spritesArr)
            {
                spritesScriptList.Add(new SpriteScript(sprite, dmg[0]));
                dmg.RemoveAt(0);
            }
        }
    }

    ///<summary>
    /// Se le ingresan a los objetos los sprites de manera aleatoria
    ///</summary>
    public void SetRandomSprites()
    {
        GameObject childGO; ///Referencia al objeto hijo de cada padre transform
        CardScript cardScript; ///Referencia al script de CardScript
        List<SpriteScript> tempSpritesScriptList; ///Lista temporal de los sprites
        List<Image> tempGOCurrentSprite = new List<Image>(); ///Lista temporal de los sprites
        Dictionary<Sprite, int> spriteDictionary = new Dictionary<Sprite, int>();

        ///Crear lista temporal de hijos
        int index = 0; ///Indice igual a cero

        ///Se hace un foreach en la lista de transformsPadre
        tempSpritesScriptList = new List<SpriteScript>(spritesScriptList); ///Si inicializa la lista cada nuevo elemento
        foreach (GameObject parent in cardsParentsList)
        {
            ///Se itera en todos los hijos de cada transform
            for (int child = 0; child < parent.transform.childCount; child++)
            {
                childGO = parent.transform.GetChild(child).gameObject; ///Se obtiene la referencia del GO hijo
                cardScript = childGO.GetComponent<CardScript>(); ///Se obtiene la referencia al script de la carta
                index = rng.Next(0, tempSpritesScriptList.Count);

                if (FindSpriteInDictionary(spriteDictionary, tempSpritesScriptList[index].sprite) < 1)
                    cardScript.CardSprite = tempSpritesScriptList[index];
                else
                {
                    cardScript.CardSprite = tempSpritesScriptList[index];
                    tempSpritesScriptList.RemoveAt(index);
                }
            }
        }
    }

    /// <summary>
    /// Se revisa en un diccionario, qué elementos de sprites ya fueron utilizados, de esta manera no se repitan dichos sprites.
    /// </summary>
    /// <param name="spriteDictionary">El diccionario a investigar</param>
    /// <param name="spriteToFind">El sprite a buscar</param>
    /// <returns></returns>
    int FindSpriteInDictionary(Dictionary<Sprite, int> spriteDictionary, Sprite spriteToFind)
    {
        ///Se pregunta si el diccionaio tiene elementos
        if(spriteDictionary.Count > 0)
        {
            ////Se itera por todos los elementos
            foreach(KeyValuePair<Sprite, int> spriteIntPair in spriteDictionary)
            {
                ///Se pregunta si la llave es igual al sprite y si su valor es menor a uno
                if(spriteIntPair.Key == spriteToFind && spriteIntPair.Value < 2)
                {
                    spriteDictionary[spriteIntPair.Key] += 1; ///aumenta en uno la llave
                    
                    return spriteDictionary[spriteIntPair.Key]; ///Regresa esta llave
                }
            }
        }
        spriteDictionary.Add(spriteToFind, 0); ///Al no encontrarse dentro del diccionario, se añade
        return spriteDictionary[spriteToFind]; ///Se regresa su valor actual

    }

    public void Restart()
    {
        SetHealthAndDamage();
        renewBoard.CheckIfBoardCanBeRenewed();
    }
    
}
