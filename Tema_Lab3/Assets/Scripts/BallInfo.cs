using UnityEngine;

public class BallInfo : MonoBehaviour
{
    private Transform hoopTarget;
    public Vector3 ThrowPosition { get; set; }

    void Start()
    {
        GameObject hoopObject = GameObject.FindGameObjectWithTag("Hoop");
        if (hoopObject != null)
        {
            hoopTarget = hoopObject.transform;
        }
        ThrowPosition = transform.position;
    }

    void Update()
    {
        if (hoopTarget != null)
        {
            float currentDistance = Vector3.Distance(transform.position, hoopTarget.position);
        }
    }
}