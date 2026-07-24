using UnityEngine;

public class Panelscript : MonoBehaviour
{
    public GameObject panel;
    public void ShowPanel()
    {
      panel.SetActive(true);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}
