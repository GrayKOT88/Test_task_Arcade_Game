using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartExit : MonoBehaviour
{   
    private Player _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void StartButton() 
    {
        _player.GameStart();
    }

    public void RestartButton() 
    {
        SceneManager.LoadScene(0);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
AppLication.Quit();
#endif
    }
}
