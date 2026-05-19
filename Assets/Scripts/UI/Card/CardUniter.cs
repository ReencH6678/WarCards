using UnityEngine;

[RequireComponent(typeof(Card))]
public class CardUniter : MonoBehaviour
{
    private Card card;

    private void Awake()
    {
        card = GetComponent<Card>();
    }

    public bool TryUniteCard(Card dropCard)
    {
        if (dropCard.Id == card.Id)
        {
            if (dropCard.Level == card.Level)
            {
                card.AddLevel();

                Destroy(dropCard.gameObject);

                return true;
            }
        }

        return false;
    }
}
