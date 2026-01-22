using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class PanelTextSetter : MonoBehaviour
{
    public TextLoader textLoader;
    public string panelKey;
    public TMP_Text textComponent;

    private bool loading = false;
    void Start()
    {
        if (textLoader != null && textComponent != null)
        {
            loading = true;
        }
    }

    private void Update()
    {
        if (loading && textLoader.isLoaded)
        {
            textComponent.text = textLoader.GetText(panelKey);
            loading = false;
        }
    }
}
