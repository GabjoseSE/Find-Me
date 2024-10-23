using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public ZombieAI[] zombieBehaviour;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public float terrorDetect = 100f;
    public float terrorSpeed = 12f;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;
            foreach (ZombieAI zombie in zombieBehaviour)
            {
                zombie.SetDetectionRange(terrorDetect);
                zombie.SetSpeed(terrorSpeed);
            }
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
