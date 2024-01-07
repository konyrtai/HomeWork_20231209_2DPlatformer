using Assets.Scripts.DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

            if (sceneBuildIndex != 0)
            {
                DataPersistenceManager.Instance.SaveGame(sceneBuildIndex);
            }
        }
    }
}