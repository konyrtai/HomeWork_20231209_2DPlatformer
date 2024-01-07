using Assets.Scripts.DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    public void NewGame()
    {
        DisableButtons();
        DataPersistenceManager.Instance.NewGame();
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        loadGameButton.interactable = DataPersistenceManager.Instance.Scene.HasValue;
    }

    public void LoadGame()
    {
        DisableButtons();

        if (DataPersistenceManager.Instance.Scene.HasValue)
        {
            SceneManager.LoadScene(DataPersistenceManager.Instance.Scene.Value);
        }
    }

    private void DisableButtons()
    {
        newGameButton.interactable = false;
        loadGameButton.interactable = false;
    }
}
