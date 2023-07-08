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
    
    
    private void Awake()
    {
        _previousPositions.Add(target.position);
        _previousTimes.Add(0);
    }
    
    private void LateUpdate()
    {
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
    }
}
