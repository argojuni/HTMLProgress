using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform Target;

    Vector3 velocity = Vector3.zero;
    public Vector3 positionOffset;
    
    [Header("Axis Limitation")]
    public Vector2 xLimit;// X axis limination
    public Vector2 yLimit;// y axis limination

    [Range(0,1)]
    public float smoothTime;

    private Animator anim;  // Komponen Animation
    private void Awake()    
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position+positionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void playcameraanimation(string animationname)
    {
        if (anim != null)
        {
            anim.SetTrigger(animationname);
        }
        else
        {
            Debug.Log("Animation not found");
        }
    }
}
