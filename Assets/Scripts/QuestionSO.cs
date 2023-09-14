using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Question Menu", order = 0)]
public class QuestionSO : ScriptableObject {
    [TextAreaAttribute(4,5)]
    [SerializeField] string question = "Enter new question text here.";

    public string GetQuestion(){
        return question;
    }
}
