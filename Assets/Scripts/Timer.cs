using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timerValue;
    public bool insAnsweringQuestion;
    public float fillFraction;
    public bool loadNextQuestion;
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer(){
        timerValue -= Time.deltaTime;

        if(insAnsweringQuestion){
            if(timerValue > 0){
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else{
                insAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else{
            if(timerValue > 0){
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else{
                insAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }

    public void CancelTimer(){
        timerValue = 0;
    }
}
