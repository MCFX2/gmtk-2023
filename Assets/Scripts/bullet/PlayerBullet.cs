using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBullet : MonoBehaviour
{
    private Camera _camera;
    private Rigidbody2D _rigidbody2D;

    private float _curVelocity = 0.0f;

    [SerializeField] private List<EffectOnDash> dashEffects = new();
    
    // [Header("Movement Settings")]
    [field: SerializeField] public float StartVelocity { get; private set; } = 1.0f;
    [SerializeField] private float velocityStep = 0.5f;
    [field: SerializeField] public float MaxVelocity { get; private set; } = 5.0f;

    [SerializeField] private float turnSpeed = 720.0f;

    [Header("Camera Settings")]
    [SerializeField] private float baseFov = 5.0f;

    private float curFov = 5.0f;

    [SerializeField] private float maxFov = 15.0f;

    [SerializeField] private Interp.Type fovScalingMethod = Interp.Type.Linear;

    [SerializeField] private float fovTransitionTime = 0.5f;
    
    // used to keep track of the camera scaling coroutine so we can cancel it
    // if the user adjusts their speed again before the transition is finished
    private Coroutine _fovCoroutine;
    
    // after this much time has passed turning the same direction, snap to the target angle
    // this is also used to work out the acceleration curve for bullet steering
    [SerializeField] private float turnSnapTime = 2.5f;
    private float _timeSteering = 0.0f;

    private int isTouching = 0;

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _camera.orthographicSize = baseFov;
        curFov = baseFov;
        
        _curVelocity = StartVelocity;
    }

    private void UpdateCameraFov(bool increase)
    {
        if (_fovCoroutine != null)
        {
            StopCoroutine(_fovCoroutine);
        }
            
        var startFov = _camera.orthographicSize;
        var fovStepSize = (maxFov - baseFov) /
            (MaxVelocity - StartVelocity) * velocityStep;
        var fovError = curFov - startFov;
        if (increase)
        {
            curFov += fovStepSize;
        }
        else
        {
            curFov -= fovStepSize;
        }
        var action = new Transition
        {
            action = Transition.Style.Custom,
            amount = (increase ? 1 : -1) * fovStepSize + fovError,
            interpolation = fovScalingMethod,
            time = fovTransitionTime,
            CustomAction = f =>
            {
                _camera.orthographicSize = startFov + f;
            }
        };
        _fovCoroutine = StartCoroutine(action.Animate());
    }

    private float realVelocity;
    private Vector2 oldPosition;
    private float holdTime = 0.0f;
    private void FixedUpdate()
    {
        realVelocity = (oldPosition - _rigidbody2D.position).magnitude / Time.fixedDeltaTime;
        oldPosition = _rigidbody2D.position;
    }

    // Update is called once per frame
    private void Update()
    {
        holdTime -= Time.deltaTime;
        // work out rotation
        var curPos = _rigidbody2D.position;
        var direction = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition) - curPos;
        var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var curAngle = _rigidbody2D.rotation;
        var dAngle = Mathf.DeltaAngle(curAngle, targetAngle);
        var maxTurn = (turnSpeed + Interp.Erp(Interp.Type.InSquared, 0, turnSpeed * 4, _timeSteering / turnSnapTime)) * Time.deltaTime;
        if (Mathf.Abs(dAngle) <= maxTurn)
        {
            _timeSteering = 0f;
            _rigidbody2D.MoveRotation(targetAngle);
        }
        else
        {
            _timeSteering += Time.deltaTime;
            _rigidbody2D.MoveRotation(curAngle + Mathf.Sign(dAngle) * maxTurn);
        }

        if (Input.GetMouseButtonDown(1) || (holdTime < 0.0f && _rigidbody2D.velocity.magnitude > realVelocity * 2))
        {
            if (_curVelocity <= StartVelocity)
            {
                // play "already minimum speed" feedback here
            }
            else
            {
                // play slowdown feedback here
                UpdateCameraFov(false);

                // update velocity
                _curVelocity = Mathf.Clamp(_curVelocity - velocityStep,
                    StartVelocity,
                    MaxVelocity);
            }
        }
        
        // work out velocity
        if (Input.GetMouseButtonDown(0))
        {
            holdTime = 0.5f;
            if (_curVelocity >= MaxVelocity)
            {
                // play "maxed out" feedback here
            }
            else
            {
                // play boost feedback here
                foreach (var effect in dashEffects)
                {
                    effect.Fire();
                }
                
                // adjust camera fov
                UpdateCameraFov(true);
            
                // update velocity
                _curVelocity = Mathf.Clamp(_curVelocity + velocityStep, StartVelocity,
                    MaxVelocity);
            }
        }

        _rigidbody2D.velocity = _rigidbody2D.transform.right * _curVelocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isTouching++;
        if (col.rigidbody.bodyType == RigidbodyType2D.Static)
        { // todo: use a tag for this instead?
            // play "hit wall" feedback here
            
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isTouching--;
    }
}
