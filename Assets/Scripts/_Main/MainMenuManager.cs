using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject helpScreen;

    private void Start()
    {
        helpScreen.transform.localScale = Vector2.zero;

        InputManager.Instance.OnCancelAction += CloseHelpScreen;
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnCancelAction -= CloseHelpScreen;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenHelpScreen()
    {
        helpScreen.SetActive(true);
        Tween.Scale(helpScreen.transform, Vector2.one, 0.2f);
    }

    public void CloseHelpScreen()
    {
        Tween.Scale(helpScreen.transform, Vector2.zero, 0.1f);
    }
}