using UnityEngine;

[RequireComponent(typeof(CardDispenser))]
public class WinHandler : MonoBehaviour
{
    [SerializeField] private Unit _enemyCastel;
    [SerializeField] private Canvas _winCanves;
    [SerializeField] private Canvas _deckUI;

    [SerializeField] private WinWindow _winWindow;
    [SerializeField] private WindowsHandler _windowHandler;

    private CardDispenser _dispenser;

    private void Awake()
    {
        _dispenser = GetComponent<CardDispenser>(); 
    }

    private void Start()
    {
        _enemyCastel.Health.Died += Win;
    }

    private void OnDisable()
    {
        _enemyCastel.Health.Died -= Win;
    }

    private void Win(Team team)
    {
        _deckUI.enabled = false;
        _winCanves.gameObject.SetActive(true);
        
        _winWindow.SetWinCards(_dispenser.DispendPrize());
        _windowHandler.Open(_winWindow);
    }

}
