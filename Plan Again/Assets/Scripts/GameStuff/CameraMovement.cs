using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform target;
    public float smoothing;
    public Vector2 MaxPosition;
    public Vector2 MinPosition;

    [Header("Animator")]
    public Animator anim;

    [Header("Position Reset")]
    public VectorValue camMin;
    public VectorValue camMax;

    // Start is called before the first frame update
    void Start()
    {
        MaxPosition = camMax.initialValue;
        MinPosition = camMin.initialValue;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, MinPosition.x, MaxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, MinPosition.y, MaxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
    public void BeginKick()
    {
        anim.SetBool("KickActive", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("KickActive", false);
    }
}
