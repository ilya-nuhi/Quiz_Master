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

    void Awake() {
        timer = FindObjectOfType<Timer>();
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindAnyObjectByType<EndScreen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if(quiz.isComplete && timer.loadNextQuestion){
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }    
    }

    public void OnReplayLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
