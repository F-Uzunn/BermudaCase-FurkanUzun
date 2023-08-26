using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [field: SerializeField] private GameData gamedata;
    void Start()
    {
#if !UNITY_EDITOR
        SaveManager.LoadData(gamedata);
#endif
        SceneManager.LoadScene(gamedata.LevelIndex);
    }
}
