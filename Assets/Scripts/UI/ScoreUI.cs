using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private float _lastUpdatedScore;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _lastUpdatedScore = 0;
        UpdateScoreText(_lastUpdatedScore);

        GameEventManager.Attach(GameEventType.ScoreUpdated, OnScoreUpdate);
    }

    private void OnScoreUpdate(object newScore)
    {
        UpdateScoreText((float)newScore);
    }

    private void UpdateScoreText(float targetScore)
    {
        DOTween.To(SetSocreText, _lastUpdatedScore, targetScore, 1f);
    }

    private void SetSocreText(float newScore)
    {
        _lastUpdatedScore = newScore;
        _scoreText.text = newScore.ToString();
    }

}
