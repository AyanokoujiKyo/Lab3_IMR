using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject ballPrefab;
    public Transform spawnPoint;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnNewBall();
    }

    public void SpawnNewBall()
    {
        Invoke("CreateBall", 1f);
    }

    private void CreateBall()
    {
        Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}