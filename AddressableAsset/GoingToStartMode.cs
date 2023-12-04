using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GoingToStartMode : MonoBehaviour
{

    public void StartScene() 
    {
        LoadingSceneController.Instance.LoadScene("StartMode");
    }

}
