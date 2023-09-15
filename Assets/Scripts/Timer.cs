using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timerValue;
    public bool insAnsweringQuestion = true;

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer(){
        timerValue -= Time.deltaTime;

        if(insAnsweringQuestion){
            if(timerValue <=0){
                insAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else{
            if(timerValue <= 0){
                insAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
            }
        }

        Debug.Log(timerValue);

    }
}
