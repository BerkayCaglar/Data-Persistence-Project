using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BestScoreTableUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
