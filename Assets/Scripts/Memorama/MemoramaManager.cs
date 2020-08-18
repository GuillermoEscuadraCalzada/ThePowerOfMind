using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MemoramaManager 
{
    #region Instances
    private static MemoramaManager instance = null;

    public static MemoramaManager Instance {
        get {
            if (instance == null) 
                instance = new MemoramaManager();
            return instance;
        }
    }

    MemoramaManager()
    {
        Start();
    }

    /// <summary>  Variable del gameManager
    /// </summary>
    GameManager gameManager;
    #endregion

   
    List<CardScript> cardsSwaped;
    /// <summary>
    /// Referencia pública de la lista de cartas volteadas
    /// </summary>
    public List<CardScript> CardsSwaped {
        get {
            return cardsSwaped;
        }
    }

    private int cardLimit = 2;
    public int CardLimit {
        get {
            return cardLimit;
        }
    }
    private bool found = false;
    private bool Found {
        get {
            return found;
        }
    }

    string currentDamageString = "Current DMG: ";

    private void Start()
    {
        gameManager = GameManager.Instance;
        cardsSwaped = new List<CardScript>();
        SetDamagesText();
    }

    public void SetDamagesText()
    {
        gameManager.playerDamageText.text = currentDamageString + gameManager.Player.Damage; ///Se actualiza el daño del jugador
        gameManager.enemyDamageText.text = currentDamageString + gameManager.Enemy.Damage; ///Se actualiza el daño del jugador
    }

    /// <summary>
    /// Detecta los toques en pantalla
    /// </summary>
    public void DetectImageTouch()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit) && cardsSwaped.Count < cardLimit)
            {
                Debug.Log("Image" + raycastHit.collider.name + "Touched");
                //cardsSwaped.Add(raycastHit.collider.gameObject);
            }
        }
    }


    public void CheckCurrentCards()
    {
        ///El contador de cartas actuales tocadas es igual al límite (2)
        if (cardsSwaped.Count.Equals(cardLimit))
        {
            CardScript firstCard, secondCard;
            List<CardScript> tempSpriteList = new List<CardScript>(cardsSwaped);
            firstCard = tempSpriteList[0]; tempSpriteList.RemoveAt(0);
            secondCard = tempSpriteList[0]; tempSpriteList.RemoveAt(0);

            ///La carta inicial es diferente a la segunda carta
            ///El sprite de las dos cartas son el mismo
            ///no están realizando ningún cambio
            if (FirstAndSecondCardBoolMethod(firstCard, secondCard) && firstCard.CardSprite.sprite == secondCard.CardSprite.sprite)
            {
                Debug.Log("Nice! You found Two Equal Cards!");
                firstCard.Selected = true;
                secondCard.Selected = true;
                gameManager.Player.Damage += firstCard.CardSprite.damage + secondCard.CardSprite.damage; ///Se suma el puntaje de los sprites al daño del jugador
                gameManager.playerDamageText.text = currentDamageString + gameManager.Player.Damage; ///Se actualiza el daño del jugador
                cardsSwaped.Clear();
            }
            ///La carta inicial es diferente a la segunda carta
            ///El sprite es diferente
            else if (FirstAndSecondCardBoolMethod(firstCard, secondCard) && firstCard.CardSprite.sprite != secondCard.CardSprite.sprite)
            {
                Debug.LogError("Sad :( You found Two Different Cards!");
                firstCard.Selected = secondCard.Selected = false;
                firstCard.Fading = secondCard.Fading = true;
                gameManager.Enemy.Damage += firstCard.CardSprite.damage + secondCard.CardSprite.damage;
                gameManager.enemyDamageText.text = currentDamageString + gameManager.Enemy.Damage; ///Se actualiza el daño del enemigo
                cardsSwaped.Clear();
            }
        }

    }

    private static bool FirstAndSecondCardBoolMethod(CardScript firstCard, CardScript secondCard)
    {
        return firstCard != secondCard && !firstCard.swapping && !secondCard.swapping;
    }

}
