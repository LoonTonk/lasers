using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int bounces = 3;
    public float offset = 0;
    Vector3[] points;
    [SerializeField] LineRenderer lineRenderer;

    void FixedUpdate()
    {
        transform.right =  (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        points = new Vector3[bounces+1];
        Vector3 dir = transform.right;
        Vector3 pos = transform.position + offset * dir;
        points[0] = pos;
        for (int i = 1; i <= bounces; i++)
        {
            RaycastHit2D ray = Physics2D.Raycast(pos + 0.01f * dir, dir);
            if (ray.collider != null)
            {
                pos = ray.point;
                points[i] = pos;
                dir = ray.normal;
            } else
            {
                points[i] = transform.position + dir * 1000f;
            }
        }

        lineRenderer.positionCount = bounces;
        lineRenderer.SetPositions(points);
    }
}
