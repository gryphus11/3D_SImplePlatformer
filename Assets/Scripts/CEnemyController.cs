using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float moveRangeY = 3.0f;
    public float downFactor = 3.0f;

    private Vector3 _initialPosition = Vector3.zero;

    private int _directionY = 1;

    // Use this for initialization
    void Start()
    {
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movementY = moveSpeed * Time.deltaTime * _directionY;

        if (_directionY == -1)
        {
            movementY *= downFactor;
        }

        float newY = transform.position.y + movementY;

        if (newY - _initialPosition.y > moveRangeY)
        {
            _directionY = -1;
        }
        else if (newY - _initialPosition.y < -moveRangeY)
        {
            _directionY = 1;
        }

        transform.position += new Vector3(0.0f, movementY, 0.0f);
    }
}
