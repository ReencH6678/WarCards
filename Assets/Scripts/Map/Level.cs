using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private SceneAsset _level;

    [field: SerializeField] public Sprite Image { get; private set; }

    public void Load()
    {
        SceneManager.LoadScene(_level.name);
    }
}
