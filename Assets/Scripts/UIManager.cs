using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable()]
public struct UIManagerParameters
{
    [Header("Answer Question")]
    [SerializeField] float _margins;
    public float margins { get { return _margins; } }
}

[Serializable()]
public struct UIElements
{
    [SerializeField] RectTransform _answersContentArea;
    public RectTransform answersContentArea { get { return _answersContentArea; } }

    [SerializeField] TextMeshProUGUI _questionInfoTextObj;
    public TextMeshProUGUI questionInfoTextObj { get { return _questionInfoTextObj; } }

    [SerializeField] TextMeshProUGUI _scoreText;
    public TextMeshProUGUI scoreText { get { return _scoreText; } }
    [Space]

    [SerializeField] Image _resolutionBG;
    public Image resolutionBG { get { return _resolutionBG; } }

    [SerializeField] TextMeshProUGUI _resolutionStateInfoText;
    public TextMeshProUGUI resolutionStateInfoText { get { return _resolutionStateInfoText; } }

    [SerializeField] TextMeshProUGUI _resolutionScoreText;
    public TextMeshProUGUI resolutionScoreText { get { return _resolutionScoreText; } }

    [Space]
    [SerializeField] TextMeshProUGUI _highScoreText;
    public TextMeshProUGUI highScoreText { get { return _highScoreText; } }

    [SerializeField] CanvasGroup _mainCanvasGroup;
    public CanvasGroup mainCanvasGroup { get { return _mainCanvasGroup; } }

    [SerializeField] RectTransform _finishUIElements;
    public RectTransform finishUIElements { get { return _finishUIElements; } }
}

public class UIManager : MonoBehaviour
{
  public enum RESOLUTIONSCREENTYPE
    {
        Correct,
        Incorrect,
        Finish
    }

    [Header("References")]
    [SerializeField] private GameEvents events;

    [Header("UI Elements(Prefabs)")]
    [SerializeField] AnswerData answerPrefab;

    [SerializeField] UIElements uIElements;

    [Space]
    [SerializeField] UIManagerParameters parameters;

    List<AnswerData> currentAnswer = new List<AnswerData>();

    private void OnEnable()
    {
        events.UpdateQuestionUI += UpdateQuestionUI;
    }
    private void OnDisable()
    {
        events.UpdateQuestionUI -= UpdateQuestionUI;
    }

   void UpdateQuestionUI(Question question)
    {
        uIElements.questionInfoTextObj.text = question.info;
        CreateAnswers(question);
    }
    void CreateAnswers(Question question)
    {
        EraseAnswer();

        float offset = 0 - parameters.margins;
        for (int i = 0; i < question.answer.Length; i++)
        {
            AnswerData newAnswer = (AnswerData)Instantiate(answerPrefab, uIElements.answersContentArea);  //Layout Group IS SLOW!!! DONT USE IT

            newAnswer.UpdateData(question.answer[i].info, i);

            newAnswer.rect.anchoredPosition = new Vector2(0, offset);
            offset -= (newAnswer.rect.sizeDelta.y + parameters.margins);

            uIElements.answersContentArea.sizeDelta = new Vector2(uIElements.answersContentArea.sizeDelta.x,offset *-1);

            currentAnswer.Add(newAnswer);


        }
    }
    void EraseAnswer()
    {
        foreach (var answer in currentAnswer)
        {
            Destroy(answer.gameObject);
        }
        currentAnswer.Clear();
    }

}
