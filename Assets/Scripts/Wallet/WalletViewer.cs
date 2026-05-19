using TMPro;
using UnityEngine;

public class WalletViewer : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _wallet.Changed += Change;
    }

    private void OnDisable()
    {
        _wallet.Changed -= Change;
    }

    private void Change(int count)
    {
        _text.text = count.ToString();
    }
}
