using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private int _leftRotation = 180;
    private int _rightRotation = 0;
    public void Rotate(Vector3 target)
    {
        Vector2 direction = transform.position - target;

        if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, _rightRotation, 0);
        else
            transform.rotation = Quaternion.Euler(0, _leftRotation, 0);

    }
}
