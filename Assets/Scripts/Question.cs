using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Answer
{
    [SerializeField] private string _info;
    public string info { get { return _info; } }

    [SerializeField] private bool _isCorrect;
    public bool isCorrect { get { return _isCorrect; } }
}

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/new Question")]
public class Question : ScriptableObject
{
    public enum ANSWERTYPE
    {
        Multi,
        Single
    }
   

   [SerializeField] private string _info=string.Empty;
    public string info { get { return _info; } }

    [SerializeField] Answer[] _answer = null;
    public Answer[] answer { get { return _answer; } }

    //Parameters
    [SerializeField] private bool _useTimer = false;
    public bool useTimer { get { return _useTimer; } }

    [SerializeField] private int _timer = 0;
    public int timer { get { return _timer; } }

    [SerializeField] private ANSWERTYPE _answerType = ANSWERTYPE.Multi;
    public ANSWERTYPE AnswerType { get { return _answerType; } }

    [SerializeField] private int _addScore = 10;
    public int addScore { get { return _addScore; } }

    public List<int> GetCorrectAnswers()
    {
        List<int> corectAnswers = new List<int>();
        for (int i = 0; i < answer.Length; i++)
        {
            if (answer[i].isCorrect)
            {
                corectAnswers.Add(i);
            }
        }
        return corectAnswers;
}
}
