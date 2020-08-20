using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteScript 
{
    public Sprite sprite;
    public int damage;

    public SpriteScript(Sprite n_Sprite, int n_damage)
    {
        sprite = n_Sprite;
        damage = n_damage;
    }
}

public class CardScript : MonoBehaviour
{

    MemoramaManager memoramaManager;
    private SpriteScript cardSprite;
    public SpriteScript CardSprite {
        get {
            return cardSprite;
        }
        set {
            cardSprite = value;
            nextSprite = cardSprite.sprite;
        }
    }
    
    private Image image;
    public Image Image {
        get {
            return image;
        }
    }

    private CardScript partner;

    [SerializeField]  private Sprite previouseSprite;
    public Sprite PrevSprite {
        get {
            return previouseSprite;
        }
        set {
            previouseSprite = value;
        }
    }
    [SerializeField] private Sprite nextSprite;
    public Sprite NextSprite {
        get {
            return nextSprite;
        }
        set {
            nextSprite = value;
        }
    }

    [SerializeField] private Sprite backOfCard;


    public CardScript()
    {

    }
    float fade = 1f;
    float difference = 1f;


    bool fading = false;
    /// <summary>
    /// Acción de cambiar imagen y el alpha d ela imagen
    /// </summary>
    public bool Fading {
        get {
            return fading;
        }
        set {
            fading = value;
        }
    }

    /// <summary>
    /// Booleano de que está haciendo un cambio el sprite
    /// </summary>
    public bool swapping = false;

    bool selected = false;
    /// <summary>
    /// La imagen está seleccionada
    /// </summary>
    public bool Selected {
        get {
            return selected;
        }
        set {
            selected = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        memoramaManager = MemoramaManager.Instance;
        image = GetComponent<Image>();
        previouseSprite = image.sprite;
        Button button = GetComponent<Button>();
        backOfCard = Resources.Load<Sprite>("Cardback");
    }



    private void FixedUpdate()
    {
        FadingAnimation();
    }

    /// <summary>
    /// Animación donde se hace invisible y cambia de sprite la carta
    /// </summary>
    private void FadingAnimation()
    {
        if (fading)
        {
            fade -= difference * Time.deltaTime;
            if (fade <= 0)
            {
                fade = 0;
                image.sprite = nextSprite;
                nextSprite = previouseSprite;
                previouseSprite = image.sprite;
                fading = !fading;
            }
            image.color = new Color(1, 1, 1, fade);
        }
        else if (!fading)
        {
            fade += difference * Time.deltaTime;
            if (fade >= 1)
            {
                fade = 1;
                swapping = false;
            }
            image.color = new Color(1, 1, 1, fade);
        }
    }
    
    /// <summary>
    /// La carta es seleccionada e inicia el proceso de animación
    /// </summary>
    public void FadeOutAction()
    {
        ///Si no está realizando la acción de fading
        //////Si no ha sido seleccionado, las cartas no han llegado a su límite y no encontró pareja, 
        if (memoramaManager.CardsSwaped.Count < memoramaManager.CardLimit && fading != true && selected != true)
        {
            fading = true; ///Indica que la está realizando
            selected = true; ///El objeto ya está seleccionado
            swapping = true; ///Está haciendo un cambio el sprite
            Debug.Log(memoramaManager.CardsSwaped.Count);
            memoramaManager.CardsSwaped.Add(this);
        }
        
    }



}
