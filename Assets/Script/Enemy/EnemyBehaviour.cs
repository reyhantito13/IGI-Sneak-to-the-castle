using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private enum State
    {
        idle,
        walking
    }

    private State currentState;

    [Header("For Player Detection")]
    [SerializeField] private Transform _playerDetector;
    [SerializeField] private float _detectionLength = 5f;
    [SerializeField] private LayerMask _whatIsPlayer;

    [Header("For Enemy Movement")]
    private float _movementSpeed = 5.0f;
    private int _idleTimer = 3;

    // Start is called before the first frame update
    void Start()
    {
        _playerDetector = gameObject.GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {

        //Check State Sekarang
        switch (currentState)
        {
            case State.idle:
                Debug.Log("Idling");
                //Idle()
                break;
            case State.walking:
                Debug.Log("Walking");
                //Patrol()
                break;
        }
    }

    void SwitchState(State state)
    {
        currentState = state;
    }

    void Flip()
    {
        gameObject.transform.Rotate(0.0f, 180.0f, 0.00f);
    }

    private void FixedUpdate()
    {
        LookingForPlayer();
    }

    bool PlayerDetected()
    {
        return Physics.Raycast(_playerDetector.position, transform.right, _detectionLength, _whatIsPlayer);
    }

    void LookingForPlayer()
    {
        if (PlayerDetected())
        {
            Debug.Log("Player Detected");
        } else if (!PlayerDetected())
        {
            Debug.Log("Where Is Player");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_playerDetector.position, new Vector3(_playerDetector.position.x + _detectionLength, _playerDetector.position.y, 0));
    }
}
