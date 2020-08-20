using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SceneTagGO
{
    DEFEATSCENE,
    GAMESCENE,
    MENUSCENE,
    VICTORYSCENE
}
public class SceneTag : MonoBehaviour
{
    [SerializeField] SceneTagGO sceneTagGO;

    public SceneTagGO SceneTagGO {
        get {
            return sceneTagGO;
        }
    }
}
