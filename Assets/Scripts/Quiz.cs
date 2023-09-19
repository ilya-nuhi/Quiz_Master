using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using System;
using UnityEngine.UIElements;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

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

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]

    [SerializeField] UnityEngine.UI.Slider progressBar;
    

    public bool isComplete;
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update() {
        
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion){
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

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
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        UnityEngine.UI.Image buttonImage;
        if(index == correctAnswerIndex){
            questionText.text = "Corrrect!";
            buttonImage = answerbuttons[index].GetComponent<UnityEngine.UI.Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
            
        }
        else{
            questionText.text = "Sorry, the correct answer was:\n" + currentQuestion.GetAnswer(correctAnswerIndex);
            buttonImage = answerbuttons[correctAnswerIndex].GetComponent<UnityEngine.UI.Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    public void onAnswerSelected(int index){
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        if(progressBar.value == progressBar.maxValue){
            isComplete = true;
        }
        
    }

    void GetNextQuestion(){
        SetButtonState(true);
        SetDefaultButtonSprites();
        GetRandomQuestion();
        DisplayQuestion();
        progressBar.value++;
        scoreKeeper.IncrementQuestionsSeen();
    }

    void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0,questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion)){
            questions.Remove(currentQuestion);
        }
        
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
        questionText.text = currentQuestion.GetQuestion();
        for(int i = 0; i< answerbuttons.Length ; i++){
            TextMeshProUGUI buttonText = answerbuttons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state){
        for(int i = 0; i< answerbuttons.Length; i++){
            UnityEngine.UI.Button button = answerbuttons[i].GetComponent<UnityEngine.UI.Button>();
            button.interactable = state;
        }
    }

}
