using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonBehaviour : MonoBehaviour {

    public void ClickRestart()
    {
        Application.LoadLevel("GameScene");
    }
}
