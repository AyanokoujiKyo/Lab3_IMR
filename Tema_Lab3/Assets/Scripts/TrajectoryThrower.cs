using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]
[RequireComponent(typeof(LineRenderer))]
public class TrajectoryThrower : MonoBehaviour
{
    private XRRayInteractor rayInteractor;
    private LineRenderer trajectoryLine;

    private IXRSelectInteractable heldInteractable = null;
    private Rigidbody heldObjectRb = null;

    [Header("Throw Settings")]
    public float launchForce = 15f;
    public float throwAngle = 45.0f;

    [Header("Trajectory Line Settings")]
    public int linePoints = 75;
    public float timeStep = 0.05f;

    void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        trajectoryLine = GetComponent<LineRenderer>();
        if (trajectoryLine != null)
        {
            trajectoryLine.enabled = false;
        }
    }

    void Update()
    {
        if (rayInteractor.hasSelection)
        {
            if (heldInteractable == null)
            {
                OnGrab();
            }

            if (Input.GetMouseButton(0))
            {
                DrawTrajectory();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Throw();
            }
            else
            {
                if (trajectoryLine != null && trajectoryLine.enabled)
                    trajectoryLine.enabled = false;
            }
        }
        else if (heldInteractable != null)
        {
            OnRelease();
        }
    }

    private void OnGrab()
    {
        heldInteractable = rayInteractor.GetOldestInteractableSelected();
        heldObjectRb = heldInteractable.transform.GetComponent<Rigidbody>();

        if (heldObjectRb != null)
        {
            heldObjectRb.isKinematic = true;
            heldObjectRb.useGravity = false;
        }
    }

    private void OnRelease()
    {
        if (heldObjectRb != null)
        {
            heldObjectRb.isKinematic = false;
            heldObjectRb.useGravity = true;
        }

        heldInteractable = null;
        heldObjectRb = null;

        if (trajectoryLine != null && trajectoryLine.enabled)
            trajectoryLine.enabled = false;
    }

    private void DrawTrajectory()
    {
        if (trajectoryLine == null || heldInteractable == null) return;

        trajectoryLine.enabled = true;
        trajectoryLine.positionCount = linePoints;

        Vector3 startPosition = heldInteractable.transform.position;
        Vector3 startVelocity = GetThrowVelocity();

        for (int i = 0; i < linePoints; i++)
        {
            float t = i * timeStep;
            Vector3 pointPosition = startPosition + startVelocity * t + 0.5f * Physics.gravity * t * t;
            trajectoryLine.SetPosition(i, pointPosition);
        }
    }

    private void Throw()
    {
        if (heldInteractable == null || heldObjectRb == null) return;

        rayInteractor.interactionManager.SelectExit(rayInteractor, heldInteractable);

        heldObjectRb.isKinematic = false;
        heldObjectRb.useGravity = true;

        Vector3 startVelocity = GetThrowVelocity();
        heldObjectRb.AddForce(startVelocity, ForceMode.Impulse);
    }

    private Vector3 GetThrowVelocity()
    {
        Quaternion angleRotation = Quaternion.AngleAxis(-throwAngle, Camera.main.transform.right);
        Vector3 finalDirection = angleRotation * Camera.main.transform.forward;

        return finalDirection.normalized * launchForce;
    }
}