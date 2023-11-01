using UnityEngine;
using UnityEngine.UI;

public class ExitBehaviour : MonoBehaviour
{
    private Button button;

    private void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void Start()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogWarning("Could not find UI.Button component within: " + name + "!");
            return;
        }

        button.onClick.AddListener(OnClick);
    }
}
