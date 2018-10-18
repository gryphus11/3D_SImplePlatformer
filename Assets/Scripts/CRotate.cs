using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRotate : MonoBehaviour
{
    public Vector3 rotateEulerSpeed = Vector3.zero;

    public bool isLocalRotate = false;
    public bool reset = false;

    private Vector3 _originalEulerRotation = Vector3.zero;

    private void Awake()
    {
        _originalEulerRotation = transform.rotation.eulerAngles;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            reset = false;
            transform.eulerAngles = _originalEulerRotation;
        }

        Vector3 deltaEuler = rotateEulerSpeed * Time.deltaTime;

        if (isLocalRotate)
        {
            transform.Rotate(deltaEuler, Space.Self);
        }
        else
        {
            transform.Rotate(deltaEuler, Space.World);
        }
    }
}
