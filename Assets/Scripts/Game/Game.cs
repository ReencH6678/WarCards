using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Unit _playerCastel;
    [SerializeField] private Unit _enemyCastel;

    [SerializeField] private Canvas _winCanves;
    [SerializeField] private Canvas _gameoverCanvas;

    private void Start()
    {
        _enemyCastel.Health.Died += Win;
        _playerCastel.Health.Died += GameOver;
    }

    private void OnDisable()
    {
        _enemyCastel.Health.Died -= Win;
        _playerCastel.Health.Died -= GameOver;
    }

    private void GameOver(Team team)
    {
        _gameoverCanvas.gameObject.SetActive(true);
    }

    private void Win(Team team)
    {
        _winCanves.gameObject.SetActive(true);
    }
}
