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
        gameManager = GameManager.Instance;
        cardsSwaped = new List<CardScript>();
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
        SetDamagesText();
    }

    public void SetDamagesText()
    {
        PlayerGO.Instance.PlayerDamage.text = currentDamageString + PlayerGO.Instance.playerInstance.Damage; ///Se actualiza el daño del jugador
        EnemyGO.Instance.EnemyDamage.text = currentDamageString + EnemyGO.Instance.enemyInstance.Damage; ///Se actualiza el daño del jugador
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
                cardsSwaped.Clear();
                Debug.Log("Nice! You found Two Equal Cards!");
                firstCard.Selected = true;
                secondCard.Selected = true;
                
                PlayerGO.Instance.playerInstance.Damage += firstCard.CardSprite.damage + secondCard.CardSprite.damage; ///Se suma el puntaje de los sprites al daño del jugador
                PlayerGO.Instance.PlayerDamage.text = currentDamageString + PlayerGO.Instance.playerInstance.Damage; ///Se actualiza el daño del jugador
            }
            ///La carta inicial es diferente a la segunda carta
            ///El sprite es diferente
            else if (FirstAndSecondCardBoolMethod(firstCard, secondCard) && firstCard.CardSprite.sprite != secondCard.CardSprite.sprite)
            {
                cardsSwaped.Clear();
                Debug.LogError("Sad :( You found Two Different Cards!");
                firstCard.Selected = secondCard.Selected = false;
                firstCard.Fading = secondCard.Fading = true;
                Debug.Log("Enemy damage: " + firstCard.CardSprite.damage.ToString() + " +  " + secondCard.CardSprite.damage.ToString() + " = " + (int)((firstCard.CardSprite.damage + secondCard.CardSprite.damage)*0.5f));
                EnemyGO.Instance.enemyInstance.Damage += (int)((firstCard.CardSprite.damage + secondCard.CardSprite.damage) * 0.5f);
                EnemyGO.Instance.EnemyDamage.text = currentDamageString + EnemyGO.Instance.enemyInstance.Damage; ///Se actualiza el daño del enemigo
            }
        }

    }

    private static bool FirstAndSecondCardBoolMethod(CardScript firstCard, CardScript secondCard)
    {
        return firstCard != secondCard && !firstCard.swapping && !secondCard.swapping;
    }

}