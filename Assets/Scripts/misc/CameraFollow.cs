using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float delay = 0.1f;
    
    private List<Vector2> _previousPositions = new();
    private List<float> _previousTimes = new();


    private float curShakeAmt;
    private float curShakeTime;
    private Vector2 shakeDelta;
    private float shakeRotDelta;
    private float curShakeRotAmt;

    [SerializeField] private Interp.Type shakeAlgorithm;

    public void Shake(float amount, float time, float spinAmt)
    {
        if(curShakeAmt <= amount)
        {
            curShakeAmt = amount;
            curShakeTime = time;
            curShakeRotAmt = spinAmt;
        }
    }

    public void StopShake()
    {
        curShakeTime = 0;
    }
    
    
    private void Awake()
    {
        _previousPositions.Add(target.position);
        _previousTimes.Add(0);
    }
    
    private void LateUpdate()
    {
        if (Time.timeScale == 0) return;
        transform.position -= (Vector3)shakeDelta;
        transform.Rotate(0, 0, -shakeRotDelta);
        shakeDelta = Vector2.zero;
        shakeRotDelta = 0;
        
        _previousPositions.Add(target.position);
        _previousTimes.Add(0);

        for (var i = 0; i < _previousTimes.Count; ++i)
        {
            _previousTimes[i] += Time.deltaTime;
            if (_previousTimes[i] >= delay)
            {
                _previousTimes.RemoveAt(i);
                _previousPositions.RemoveAt(i);
                --i;
            }
        }

        if (_previousPositions.Count == 0)
        {
            return;
        }

        // set position to average of previous positions
        var newPos = _previousPositions.Aggregate(Vector2.zero, (current, pos) => current + pos) / _previousPositions.Count;
        transform.position = new Vector3(
            (newPos.x + transform.position.x) / 2, 
            (newPos.y + transform.position.y) / 2, 
            transform.position.z);
        
        // shake
        if (curShakeTime > 0)
        {
            curShakeTime -= Time.deltaTime;
            var shakeX = Interp.Erp(shakeAlgorithm, 0, curShakeAmt, Random.value);
            var shakeY = Interp.Erp(shakeAlgorithm, 0, curShakeAmt, Random.value);
            shakeRotDelta = Interp.Erp(shakeAlgorithm, 0, curShakeRotAmt, Random.value);
            if (Random.value > 0.5f)
            {
                shakeX *= -1;
            }
            if (Random.value > 0.5f)
            {
                shakeY *= -1;
            }
            if (Random.value > 0.5f)
            {
                shakeRotDelta *= -1;
            }
            shakeDelta = new Vector2(shakeX, shakeY);
            transform.position += (Vector3)shakeDelta;
            transform.Rotate(0, 0, shakeRotDelta);
        }
        else
        {
            curShakeAmt = 0;
        }
    }
}
