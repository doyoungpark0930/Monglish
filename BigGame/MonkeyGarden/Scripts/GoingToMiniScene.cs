using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToMiniScene : MonoBehaviour
{
    public void QuizGameScene()
    {
        AddLoadingSceneController.Instance.LoadScene("Assets/Scene/QuizGame.unity");
    }
    public void RunGameScene()
    {
        AddLoadingSceneController.Instance.LoadScene("Assets/Scene/RunGame.unity");
    }
    public void JumpGameScene()
    {
        AddLoadingSceneController.Instance.LoadScene("Assets/Scene/JumpGame.unity");
    }

}
