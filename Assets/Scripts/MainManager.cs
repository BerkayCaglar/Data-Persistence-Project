using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainManager : MonoBehaviour
{
    [System.Serializable]
    public class dataSave
    {
        public string playerName;
        public int score;
    }
    dataSave data = new dataSave();
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public Text ScoreText;
    public Text playerNameText;
    public GameObject GameOverText;
    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;

    public bool entered=false;
    
    // Start is called before the first frame update
    void Start()
    {
        readJson();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {   
                SceneManager.LoadScene(2);
            }
        }
    }

    public void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        data.score = m_Points;
        writeToJSON();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
    public void writeToJSON()
    {
        string path = "C:/Users/user/Desktop/saveFile.json";
        string json=JsonUtility.ToJson(data);
        File.WriteAllText(path,json);
    }

    public void readJson()
    {
        string path = "C:/Users/user/Desktop/saveFile.json";
        if(File.Exists(path))
        {
            string jsonFile = File.ReadAllText(path);
            data.playerName=JsonUtility.FromJson<dataSave>(jsonFile).playerName;
            data.score=JsonUtility.FromJson<dataSave>(jsonFile).score;
        }
        else
        { // Değişkenlerin static olayını bir dene
            data.playerName = MainUIManager.Instance.playerName.text;
        }
        playerNameText.text = data.playerName + ":" +data.score;
    }
}
