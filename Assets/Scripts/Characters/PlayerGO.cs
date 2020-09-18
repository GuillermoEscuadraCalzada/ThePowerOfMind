using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class PlayerGO : MonoBehaviour
{
    private static PlayerGO instance = null;
    public static PlayerGO Instance {
        get {
            return instance;
        }
    }
    public Player playerInstance;

    #region TextMesh
    [SerializeField] TextMeshProUGUI playerHealth;
    [SerializeField] TextMeshProUGUI playerDamage;

    public TextMeshProUGUI PlayerDamage {
        get {
            return playerDamage;
        }
    }

    public TextMeshProUGUI PlayerHealth {
        get {
            return playerHealth;
        }
    }
    #endregion


    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            playerInstance = Player.Instance;
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
