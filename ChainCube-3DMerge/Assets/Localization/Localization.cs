using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class Localization : MonoBehaviour
{
    [SerializeField] private string DEFAULT_LANGUAGE = "English";

    private Dictionary<string, string> _texts;
    private string _currentLanguage;

    public delegate void LanguageChangedEventHandler();
    public event LanguageChangedEventHandler LanguageChanged;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("LAST_LANGUAGE"))
        {
            string newLang = PlayerPrefs.GetString("LAST_LANGUAGE");
            try
            {
                SetLocalization(newLang);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                Debug.Log("Trying Default Language: " + DEFAULT_LANGUAGE);
                SetLocalization(DEFAULT_LANGUAGE);
            }
        }
        else
        {
            SetLocalization(DEFAULT_LANGUAGE);
        }
    }

    public void SetLocalization(string language)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Locale_" + language);
        if (textAsset != null)
        {
            _texts = JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.text);
            _currentLanguage = language;
            OnLanguageChanged();
        }
        else
        {
            throw new Exception("Localization Error!: " + language + " does not have a .txt resource!");
        }
    }

    public string GetText(string identifier)
    {
        if (!_texts.ContainsKey(identifier))
        {
            Debug.Log("Localization Error!: " + identifier + " does not have an associated string!");
            return null;
        }
        return _texts[identifier];
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LAST_LANGUAGE", _currentLanguage);
    }

    protected virtual void OnLanguageChanged()
    {
        if (LanguageChanged != null)
            LanguageChanged();
    }
}
