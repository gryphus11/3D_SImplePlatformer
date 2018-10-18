using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerController : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float jumpForce = 1.0f;

    public AudioSource coinSound = null;

    private Rigidbody _rigidbody = null;
    private Collider _collider = null;

    private bool _isJumpPressed = false;

    private float _cameraDistanceZ = 6.0f;
    private float _minY = -1.5f;

    private Vector3 _playerSize = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _playerSize = _collider.bounds.size;

        FollowPlayerCamera();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        FollowPlayerCamera();
        CheckFallOff();
    }

    private void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(hAxis, 0.0f, vAxis);
        Vector3 movement = moveDirection.normalized * walkSpeed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(transform.position + movement);

        if (hAxis != 0 || vAxis != 0)
        {
            Vector3 direction = new Vector3(hAxis, 0.0f, vAxis);
            _rigidbody.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void Jump()
    {
        float jumpAxis = Input.GetAxis("Jump");

        if (jumpAxis > 0.0f)
        {
            bool isGround = CheckGround();

            if (_isJumpPressed || !isGround)
            {
                return;
            }

            _isJumpPressed = true;
            Vector3 jumpVector = new Vector3(0.0f, jumpForce * jumpAxis, 0.0f);
            _rigidbody.AddForce(jumpVector, ForceMode.VelocityChange);
        }
        else
        {
            _isJumpPressed = false;
        }
    }

    private void CheckFallOff()
    {
        if (transform.position.y < _minY)
        {
            CGameManager.instance.GameOver();
        }
    }

    private void FollowPlayerCamera()
    {
        Vector3 cameraPosition = Camera.main.transform.position;

        cameraPosition.z = transform.position.z - _cameraDistanceZ;

        Camera.main.transform.position = cameraPosition;

        Camera.main.transform.LookAt(transform, Vector3.up);
    }

    private bool CheckGround()
    {
        Vector3 corner1 = transform.position + (new Vector3(_playerSize.x / 2.0f, -_playerSize.y / 2.0f + 0.01f, _playerSize.z / 2.0f));
        Vector3 corner2 = transform.position + (new Vector3(-_playerSize.x / 2.0f, -_playerSize.y / 2.0f + 0.01f, _playerSize.z / 2.0f));
        Vector3 corner3 = transform.position + (new Vector3(_playerSize.x / 2.0f, -_playerSize.y / 2.0f + 0.01f, -_playerSize.z / 2.0f));
        Vector3 corner4 = transform.position + (new Vector3(-_playerSize.x / 2.0f, -_playerSize.y / 2.0f + 0.01f, -_playerSize.z / 2.0f));

        bool grounded1 = Physics.Raycast(corner1, -Vector3.up, 0.01f);
        bool grounded2 = Physics.Raycast(corner2, -Vector3.up, 0.01f);
        bool grounded3 = Physics.Raycast(corner3, -Vector3.up, 0.01f);
        bool grounded4 = Physics.Raycast(corner4, -Vector3.up, 0.01f);

        return (grounded1 || grounded2 || grounded3 || grounded4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Coin!");
            CGameManager.instance.AddScore(1);

            Destroy(other.gameObject);

            if (coinSound != null)
            {
                coinSound.Play();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy!");
            CGameManager.instance.GameOver();
        }
        else if (other.CompareTag("Goal"))
        {
            CGameManager.instance.IncreaseLevel();
            Debug.Log("You made it!");
        }
    }
}
