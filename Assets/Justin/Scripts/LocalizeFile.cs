using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Localize File", fileName = "New Localize File")]
public class LocalizeFile : ScriptableObject
{
    public string markupToReplace;
    public Languages multiLanguageText;

    [System.Serializable]
    public class Languages
    {
        public string englishTxt;
        public string dutchTxt;
    }
}
