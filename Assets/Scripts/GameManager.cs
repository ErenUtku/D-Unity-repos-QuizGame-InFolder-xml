using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Question[] _question = null;
    public Question[] questions { get { return _question; } }

    [SerializeField] GameEvents events = null;


    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> finishedQuestion = new List<int>();
    private int currentQuestion = 0;

    void Start()
    {
        LoadQuestion();
        foreach (var Question in questions)
        {
            Debug.Log(Question);
        }


        var seed = Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);


        Display();
    }

    public void EraseAnswer()
    {
        PickedAnswers = new List<AnswerData>();
    }


    void Display()
    {
        EraseAnswer();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        }
        else
        {
            Debug.LogWarning("Something went wrong, while trying to display new Question UI Data. GameEvents.UpdateQuestionUI is null. Issue occured in GameManager.Display() method.");
        }
    }

    Question GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionInxed();
        currentQuestion = randomIndex;

        return questions[currentQuestion];
    }

    int GetRandomQuestionInxed()
    {
        var random = 0;
        if (finishedQuestion.Count < questions.Length)
        {
            do
            {
                random = Random.Range(0,questions.Length);
            } while (finishedQuestion.Contains(random)|| random==currentQuestion);
        }
        return random;
    }

    void LoadQuestion()
    {
        Object[] objs = Resources.LoadAll("Questions",typeof(Question));
        _question = new Question[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _question[i] = (Question) objs[i];
        }
    }
}
