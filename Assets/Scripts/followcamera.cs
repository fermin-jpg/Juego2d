using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading;

public class followcamera : MonoBehaviour
{
    public Transform target;
    public float speed = 12.0f;
    public float proportionalFollowDistance = 5.0f;

    public bool enableBounds = false;
    public Vector2 boundsOrigin = new Vector2(0, 0);
    public Vector2 boundsExtension = new Vector2(100, 100);

    public float targetSize = 5;

    Camera cameraComponent;


    // Start is called before the first frame update
    void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }

    /*void OnDrawGizmos()
    {
        Vector3 boundsOrigin3 = boundsOrigin;
        Vector3 boundsExtension3 = boundsExtension;

        if (enableBounds)
        {
            Handles.color = Color.white;
            Handles.DrawWireCube(boundsOrigin3, boundsExtension);
        }
    }*/

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;
        transform.position += new Vector3(0, 0, -10);
        /*
        Vector2 distance2 = target.position - transform.position;

        Vector2 displacement2 = distance2.normalized * speed * Time.deltaTime * distance2.magnitude / proportionalFollowDistance;

        if (displacement2.magnitude > distance2.magnitude) { displacement2 = distance2; }

        Vector3 displacement3 = displacement2;


        transform.position = transform.position + displacement3;

        if (enableBounds)
        {
            Vector3 p = transform.position;
            float size = cameraComponent.orthographicSize;

            if (p.x - size < boundsOrigin.x - boundsExtension.x / 2) { p = new Vector3(boundsOrigin.x - boundsExtension.x / 2 + size, p.y, p.z); }
            if (p.x + size > boundsOrigin.x + boundsExtension.x / 2) { p = new Vector3(boundsOrigin.x + boundsExtension.x / 2 - size, p.y, p.z); }
            if (p.y - size < boundsOrigin.y - boundsExtension.y / 2) { p = new Vector3(p.x, boundsOrigin.y - boundsExtension.y / 2 + size, p.z); }
            if (p.y + size > boundsOrigin.y + boundsExtension.y / 2) { p = new Vector3(p.x, boundsOrigin.y + boundsExtension.y / 2 - size, p.z); }

            transform.position = p;
        }

        cameraComponent.orthographicSize += (targetSize - cameraComponent.orthographicSize) * Time.deltaTime;
        */
    }
}