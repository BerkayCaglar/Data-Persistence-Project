using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
    using UnityEditor;
#endif
public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Ins;
    public TMP_Text nameText;
    public GameObject alertText;
    public void BestScores()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        if(nameText.text.Length <= 1)
        {
            alertText.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
           
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
    private void Awake() {
        if(Ins != null)
        {
            Destroy(gameObject);
        }
        Ins = this;
        DontDestroyOnLoad(gameObject);
    }
}
