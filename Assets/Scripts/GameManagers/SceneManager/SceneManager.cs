using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance = null;
    
    [SerializeField] SceneTagGO currentScene;
    public SceneTagGO CurrentScene {
        get {
            return currentScene;
        }
    }
    public static SceneManager Instance {
        get {
            return instance;
        }
    }
    SceneTag sceneSelector;
    SceneManager()
    {

    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            ShowScene(currentScene);
        }
        else
        {
            Destroy(this);
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

    public void ShowScene(SceneTagGO sceneTagGO)
    {
        foreach(Transform child in transform)
        {
            sceneSelector = child.GetComponent<SceneTag>();
            if(sceneSelector != null)
            {
                if (sceneSelector.SceneTagGO.Equals(sceneTagGO)) child.gameObject.SetActive(true);
                else child.gameObject.SetActive(false);
            }
        }
        currentScene = sceneTagGO;
    }

}
