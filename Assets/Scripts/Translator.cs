using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool GetLang();

    [TextArea(2,3)][SerializeField] private string _engText;

    private void Awake()
    {
        if (!Application.isEditor)
        {
            if (GetLang())
            {
                GetComponent<Text>().text = _engText;
            }
        }
    }
}
