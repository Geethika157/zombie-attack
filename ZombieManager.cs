using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance;
    public ZombieWaveData waveData;
    public int currentRoundNumber;
    public Animator busAnimator;
    public Transform bus;
    public Vector3 busStartingPos;
    public TextMeshProUGUI zombieCounter;
    public int zombieKillsCount;
    public int maxNoOfZombies;
    public GameObject surviorInfoObj;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        busStartingPos = bus.position;
        LoadWave1();
    }
    public void UpdateZombieCount()
    {
        zombieKillsCount++;
        UpdateZombieDeathText();
    }
    public void UpdateZombieDeathText()
    {
        zombieCounter.text = zombieKillsCount.ToString() + "/" + maxNoOfZombies.ToString();
    }
    // Start is called before the first frame update
    public void CheckForWaveEnd()
    {
        if (waveData.zombieDetails[currentRoundNumber].zombies.Count > 0)
        {
            return;
        }
        waveData.zombieDetails[currentRoundNumber].waveCompleted = true;
        Debug.Log("Wave ended");
        surviorInfoObj.SetActive(true);
        if (currentRoundNumber >= waveData.zombieDetails.Count - 1)
        {
            Debug.Log("Game Ended");
          // GameManager.instance.ShowEndPage(true);  // Show win panel
            return;
        }
        
        //Invoke(nameof(StartNextRound), 3);
        //Invoke(nameof(StartWaveAfterDelay), 5);
    }
    public void StartNextRound()
    {
        GameManager.instance.ShowWaitingScreen(true);
    }
    public void RemoveZombieFromWave(GameObject zombie)
    {
        waveData.zombieDetails[currentRoundNumber].zombies.Remove(zombie);
    }
    public void StartWaveAfterDelay()
    {
        Debug.Log("Next wave started" + currentRoundNumber);
        currentRoundNumber++;
        waveData.zombieDetails[currentRoundNumber].LoadZombies();
        zombieKillsCount = 0;
        surviorInfoObj.SetActive(false);
        maxNoOfZombies = waveData.zombieDetails[currentRoundNumber].zombies.Count;
        UpdateZombieDeathText();
        bus.position = busStartingPos;
        busAnimator.SetBool("StartBus", false);
    }
    public void LoadWave1()
    {
        waveData.zombieDetails[currentRoundNumber].LoadZombies();
        maxNoOfZombies = waveData.zombieDetails[currentRoundNumber].zombies.Count;
        UpdateZombieDeathText();
    }
}
[Serializable]
public class ZombieWaveData
{
    public List<ZombieDetails> zombieDetails = new List<ZombieDetails>();
}
[Serializable]
public class ZombieDetails
{
    public List<GameObject> zombies = new List<GameObject>();
    public bool waveCompleted;
    public void LoadZombies()
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            zombies[i].gameObject.SetActive(true);
        }
        
    }
}