using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator doorAnimator;
    public bool didDoorOpened;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //open door.
            int currwave = ZombieManager.instance.currentRoundNumber;
            if (!didDoorOpened && ZombieManager.instance.waveData.zombieDetails[currwave].waveCompleted)
            {
                doorAnimator.SetBool("OpenDoor",true);
                didDoorOpened = true;
            }
        }
    }
}