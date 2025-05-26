using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public float velocity = .1f;
    private List<Vector2> targets;
    private Vector2 speed = Vector2.zero;
    private int currentTargetId = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targets = new List<Vector2>
        {
            new (2, 2),
            new (-5, 0),
            new (8, -1),
            new (-2, -1),
            new (2, 0)
        };
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();

        var direction = (targets[currentTargetId] - new Vector2(transform.position.x, transform.position.y)).normalized;
        speed = direction * velocity;
        Debug.DrawLine(transform.position, targets[currentTargetId], Color.red);
        Debug.DrawLine(targets[currentTargetId], targets[NextTargetId()], Color.black);
    }

    void FixedUpdate()
    {
        transform.position += (Vector3)speed * Time.deltaTime;
    }

    bool ReachCurrentTarget()
    {
        return ((Vector2)transform.position - targets[currentTargetId]).magnitude <= .1;
    }

    private void UpdateTarget()
    {
        if (ReachCurrentTarget())
        {
            currentTargetId = NextTargetId();
        }
    }

    int NextTargetId()
    {
        return (currentTargetId + 1) % targets.Count;
    }
}
