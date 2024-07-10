using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider playerHealthSlider;
    public TextMeshProUGUI healthPercentText;
    public GameObject WaitingScreen;
    public TextMeshProUGUI waitingScreenText;
    public float waitingTime;
    public float elapsedTime;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ShowWaitingScreen(false);
    }
    // Start is called before the first frame update
    public void UpdatePlayerHealthUI(float damageValue)
    {
        playerHealthSlider.value = damageValue;
        healthPercentText.text = damageValue.ToString();

    }
    public void ShowWaitingScreen(bool nextwave)
    {
        WaitingScreen.SetActive(true);
        StartCoroutine(StartCountDown(nextwave));
    }
    IEnumerator StartCountDown(bool nextWave)
    {
        while (elapsedTime < waitingTime)
        {
            elapsedTime += Time.deltaTime;
            int timer = (int)(waitingTime - elapsedTime);
            waitingScreenText.text = timer.ToString();
            yield return null;
        }
        WaitingScreen.SetActive(false);
        elapsedTime = 0;
        if (nextWave)
        {
            ZombieManager.instance.StartWaveAfterDelay();
        }
    }
}