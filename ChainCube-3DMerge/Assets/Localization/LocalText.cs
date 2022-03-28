using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalText : MonoBehaviour
{
    [SerializeField] private string _textID;
    [SerializeField] private bool _autoUpdate = true;

    private Localization _localization;

    private TMP_Text _textComponent;

    private void Awake()
    {
        _textComponent = GameObject.FindWithTag("Localization").GetComponent<TextMeshProUGUI>();
        _localization = GetComponent<Localization>();

        if (_autoUpdate == true)
        {
            _localization.LanguageChanged += UpdateLocale;
        }
    }

    private void Start()
    {
        UpdateLocale();
    }

    public void UpdateLocale()
    {
        try
        {
            string response = _localization.GetText(_textID);
            if (response != null)
            {
                _textComponent.text = response;
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
        }
    }
}
