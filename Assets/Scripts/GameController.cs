using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject demonContainer, hudContainer, gameOverPanel;
    public TextMeshProUGUI demonCounter, timeCounter, countDownText;
    public bool gamePlaying { get; private set; }
    public int countDownTime;

    private int numTotalDemons, numSlayedDemon;
    private float startTime, elapsedTime;
    TimeSpan timePlaying;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        numTotalDemons = demonContainer.transform.childCount;
        numSlayedDemon = 0;
        demonCounter.text = "Demons: 0 / " + numTotalDemons;
        timeCounter.text = "Time: 00:00.00";

        gamePlaying = false;
        StartCoroutine(CountDownToStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlaying)
        {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    private void BeginGame()
    {
        gamePlaying = true;
        startTime = Time.time;
    }

    public void SlayDemon()
    {
        numSlayedDemon++;

        string demonCounterStr = "Demons: " + numSlayedDemon + " / " + numTotalDemons;
        demonCounter.text = demonCounterStr;

        if (numSlayedDemon >= numTotalDemons)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gamePlaying = false;
        Invoke("ShowGameOverScreen", 1.25f);
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        hudContainer.SetActive(false);
        string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
        gameOverPanel.transform.Find("FinalTimeText").GetComponent<TextMeshProUGUI>().text = timePlayingStr;
        gameOverPanel.transform.Find("RestartButton").GetComponent<Button>().Select();
    }

    IEnumerator CountDownToStart() {
        while(countDownTime > 0) {
            countDownText.text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime --;
        }

        BeginGame();
        countDownText.text = "GO!!";

        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
    }

    public void OnButtonLoadLevel(string levelToLoad) {
        SceneManager.LoadScene(levelToLoad);
    }
}
