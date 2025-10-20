using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    public enum TriggerType { Top, Bottom }
    public TriggerType type;

    private BasketballHoop parentHoop;

    void Awake()
    {
        parentHoop = GetComponentInParent<BasketballHoop>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parentHoop != null)
        {
            ThrowableObject ball = other.GetComponent<ThrowableObject>();
            if (ball != null)
            {
                if (type == TriggerType.Top)
                    parentHoop.BallEnteredTop(ball);
                else if (type == TriggerType.Bottom)
                    parentHoop.BallEnteredBottom(ball);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (parentHoop != null && other.GetComponent<ThrowableObject>() != null)
        {
            if (type == TriggerType.Top)
                parentHoop.BallExitedTop();
        }
    }
}