using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThrowableObject>() != null)
        {
            Destroy(other.gameObject);
            GameManager.Instance.SpawnNewBall();
        }
    }
}