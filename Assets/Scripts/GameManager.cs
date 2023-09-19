using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Quiz quiz;
    EndScreen endScreen;
    [SerializeField] UnityEngine.UI.Slider slider;
    Timer timer;

    GameObject startScene;

    void Awake() {
        timer = FindObjectOfType<Timer>();
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
        startScene = GameObject.Find("StartScreenCanvas");
    }

    void Start()
    {
        timer.gameObject.SetActive(false);
        startScene.SetActive(true);
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if(quiz.isComplete && timer.loadNextQuestion){
            timer.gameObject.SetActive(false);
            startScene.SetActive(false);
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnStartQuiz(){
        timer.gameObject.SetActive(true);
        startScene.SetActive(false);
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }
}
