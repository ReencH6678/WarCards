using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool IsFull { get; private set; }

    public void Put()
    {
        IsFull = true;

    }

    public void Pull()
    {
        IsFull = false;
    }

}
