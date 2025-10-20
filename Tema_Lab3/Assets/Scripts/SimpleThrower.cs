using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleThrower : MonoBehaviour
{
    private XRRayInteractor rayInteractor;
    public float minThrowForce = 10f;
    public float maxThrowForce = 30f;
    public float maxChargeTime = 2f;

    private float currentChargeTime = 0f;
    private bool isCharging = false;
    private Transform heldObject = null;

    void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    void Update()
    {
        if (rayInteractor.hasSelection)
        {
            if (heldObject == null)
            {
                heldObject = rayInteractor.GetOldestInteractableSelected().transform;
            }

            if (Input.GetMouseButtonDown(0))
            {
                isCharging = true;
                currentChargeTime = 0f;
            }

            if (isCharging && Input.GetMouseButton(0))
            {
                currentChargeTime += Time.deltaTime;
            }

            if (isCharging && Input.GetMouseButtonUp(0))
            {
                Throw();
            }
        }
        else
        {
            heldObject = null;
            isCharging = false;
        }
    }

    private void Throw()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb == null) return;

        rayInteractor.interactionManager.SelectExit(rayInteractor, (IXRSelectInteractable)rayInteractor.GetOldestInteractableSelected());

        float chargePercentage = Mathf.Clamp01(currentChargeTime / maxChargeTime);

        float finalForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargePercentage);

        Vector3 throwDirection = Camera.main.transform.forward;

        rb.AddForce(throwDirection * finalForce, ForceMode.Impulse);

        Debug.Log("Aruncat cu puterea: " + finalForce);

        isCharging = false;
        heldObject = null;
    }
}