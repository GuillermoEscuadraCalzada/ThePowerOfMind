using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenewBoard
{
    private static RenewBoard instance = null;

    public static RenewBoard Instance {
        get {
            if (instance == null) instance = new RenewBoard();
            return instance;
        }
    }

    GameManager gameManager;
    RenewBoard()
    {
        gameManager = GameManager.Instance;
        children = new List<GameObject>();
    }

    int swappingDetector = 0;
    List<GameObject> children;
    CardScript cardScript;
    bool checkingCards = false;

    public void GetBoardChildren()
    {
        foreach (GameObject parent in gameManager.CardsParentsList)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
                children.Add(parent.transform.GetChild(i).gameObject);
        }
    }

    public void Update()
    {
        if (checkingCards)
        {
            foreach (GameObject child in children)
            {
                cardScript = child.GetComponent<CardScript>();
                if (cardScript.CardSprite.sprite != cardScript.Image.sprite)
                {
                    swappingDetector++;
                }
            }
            if (swappingDetector >= children.Count)
            {
                gameManager.SetRandomSprites();
                swappingDetector = 0;
                checkingCards = !checkingCards;
            }
            else
            {
                swappingDetector = 0;
            }
        }
    }
    
    public void RemakeBoard()
    {
        if(checkingCards != true)
        {
            foreach (GameObject parent in gameManager.CardsParentsList)
            {
                for (int i = 0; i < parent.transform.childCount; i++)
                {
                    cardScript = parent.transform.GetChild(i).gameObject.GetComponent<CardScript>();
                    if(cardScript.Fading)
                    {
                        Debug.LogError("Wait until it fades completely");
                        return;
                    }
                    ///La carta está boca arriba
                    if (cardScript.CardSprite.sprite == cardScript.Image.sprite)
                    {
                        cardScript.Selected = false;
                        cardScript.Fading = true;
                        gameManager.MemoramaManager.CardsSwaped.Clear();
                    }
                }
            }
            checkingCards = true;
        }
    }


}
