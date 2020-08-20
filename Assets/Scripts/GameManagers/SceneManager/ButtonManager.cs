using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBackGameScene()
    {
        parent.gameObject.SetActive(false);
        SceneManager.Instance.ShowScene(SceneTagGO.GAMESCENE);
    }

}
