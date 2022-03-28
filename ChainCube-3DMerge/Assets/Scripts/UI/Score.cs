using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _prefabText;

    private TMP_Text _textScore;
    private long _currentScore = 0;

    public static Score Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _textScore = GetComponent<TextMeshProUGUI>();
    }

    public void SpawnPopUpText(long points, Vector3 position)
    {
        GameObject popUp = Instantiate(_prefabText, position, Quaternion.identity).gameObject;

        if (popUp.TryGetComponent<TextMeshPro>(out TextMeshPro popUpText))
        {
            popUpText.text = $"+{points}";
        }
    }

    public void UpdateScore(long score)
    {
        _currentScore = _currentScore + score;
        _textScore.text = "Score: " + _currentScore;
    }
}
