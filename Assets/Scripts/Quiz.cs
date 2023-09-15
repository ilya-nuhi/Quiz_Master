using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerbuttons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Controls")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] UnityEngine.UI.Image timerImage;
    Timer timer;
    
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
    }

    void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion){
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion=false;
        }
        else if(!hasAnsweredEarly && !timer.insAnsweringQuestion ){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void DisplayAnswer(int index){
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        UnityEngine.UI.Image buttonImage;
        if(index == correctAnswerIndex){
            questionText.text = "Corrrect!";
            buttonImage = answerbuttons[index].GetComponent<UnityEngine.UI.Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else{
            questionText.text = "Sorry, the correct answer was:\n" + question.GetAnswer(correctAnswerIndex);
            buttonImage = answerbuttons[correctAnswerIndex].GetComponent<UnityEngine.UI.Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    public void onAnswerSelected(int index){
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();

    }

    void GetNextQuestion(){
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    void SetDefaultButtonSprites()
    {
        UnityEngine.UI.Image buttonImage;
        for(int i =0; i<answerbuttons.Length; i++){
            buttonImage = answerbuttons[i].GetComponent<UnityEngine.UI.Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void DisplayQuestion(){
        questionText.text = question.GetQuestion();
        for(int i = 0; i< answerbuttons.Length ; i++){
            TextMeshProUGUI buttonText = answerbuttons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    void SetButtonState(bool state){
        for(int i = 0; i< answerbuttons.Length; i++){
            Button button = answerbuttons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

}
