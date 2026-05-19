using UnityEngine;

public class UnitPreview : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _viewer;

    private Card _card;
    private CardDrager _cardDrager;

    private bool _isOn;

    private void Awake()
    {
        _card = GetComponent<Card>();
        _cardDrager = GetComponent<CardDrager>();
        _viewer = Instantiate(_viewer);
    }

    private void OnEnable()
    {
        _viewer.gameObject.SetActive(false);
        _cardDrager.OnStartDrag += SetEnabled;
        _cardDrager.OnDragEnd += SetDisabled;
    }

    private void OnDisable()
    {
        _cardDrager.OnStartDrag -= SetEnabled;
        _cardDrager.OnDragEnd -= SetDisabled;
    }

    private void SetEnabled()
    {
        _viewer.gameObject.SetActive(true);

        _isOn = true;
    }

    private void SetDisabled()
    {
        _viewer.gameObject.SetActive(false);
        _isOn = false;
    }

    private void Update()
    {
        if (_isOn)
        {
            if (_viewer.sprite == null)
            {
                if (_card.UnitPrefabe.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    _viewer.sprite = spriteRenderer.sprite;
                    _viewer.transform.localScale = _card.UnitPrefabe.transform.localScale;
                }
            }
            else
            {
                Vector3 worldPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                _viewer.transform.position = worldPosition;
            }
        }
    }
}
