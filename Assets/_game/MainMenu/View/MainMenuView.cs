using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _optionsBtn;
    [SerializeField] private Button _exitBtn;

    private void OnEnable()
    {
        _playBtn.onClick.AddListener(OnStartGame);
        _optionsBtn.onClick.AddListener(OnOpenOptions);
        _exitBtn.onClick.AddListener(OnExitGame);
    }

    private void OnDisable()
    {
        _playBtn.onClick.RemoveListener(OnStartGame);
        _optionsBtn.onClick.RemoveListener(OnOpenOptions);
        _exitBtn.onClick.RemoveListener(OnExitGame);
    }

    private void OnStartGame()
    {
        print("start game");
        SceneManager.LoadScene(1);
    }

    private void OnOpenOptions()
    {
        print("open options");
    }

    private void OnExitGame()
    {
        print("exit");
        Application.Quit();
    }
}
