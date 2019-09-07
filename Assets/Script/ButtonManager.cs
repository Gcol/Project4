using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public void LoadByIndex(int sceneIndex)
    {
          SceneManager.LoadScene(sceneIndex);
    }


    public void Quit () {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit ();
    #endif
    }
}

RE dras my life 
