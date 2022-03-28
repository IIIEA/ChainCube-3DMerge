using System.Collections.Generic;
using UnityEngine;

public class Implementation : MonoBehaviour
{
    [SerializeField] private List<string> _languages;

    private Localization _localization;
    private int _index = 0;

    private void Start()
    {
        _localization = GetComponent<Localization>();
    }

    public void OnLanguageButtonPressed()
    {
        _index++;
        if (_index == _languages.Count)
        {
            _index = 0;
        }
        _localization.SetLocalization(_languages[_index]);
    }
}
