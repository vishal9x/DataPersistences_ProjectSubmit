using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreNameText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        //if(MenuManager.instance.HighestScore<m_Points)
        //{
        //    MenuManager.instance.HighestScore = m_Points;
        //    BestScoreNameText.text = "Best score: " + MenuManager.instance.PlayerName + ": " + m_Points;
        //}

        print("start MenuManager.instance.HighestScore:="+ MenuManager.instance.HighestScore);
        print("start m_Points:=" + m_Points);
        if(MenuManager.instance.HighestScore>m_Points)
        {
            BestScoreNameText.text = "Best score: " + MenuManager.instance.PlayerName + ": " + MenuManager.instance.HighestScore;
        }


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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if(m_Points>0)
        {
            print("end MenuManager.instance.HighestScore:=" + MenuManager.instance.HighestScore);
            print("end m_Points:=" + m_Points);
            if (m_Points>MenuManager.instance.HighestScore)
            {
                BestScoreNameText.text = "Best score: " + MenuManager.instance.currentPlayerName + ": " + m_Points;
                MenuManager.instance.PlayerName = MenuManager.instance.currentPlayerName;
                MenuManager.instance.HighestScore = m_Points;
            }
            //else
            //{
            //    BestScoreNameText.text = "Best score: "+MenuManager.instance.PlayerName+": "+ m_Points;
            //}
        }

        MenuManager.instance.SaveGameData();
    }
}
