using System;
using System.Collections.Generic;
using UnityEngine;

public class PickerUpper : MonoBehaviour
{
    [Tooltip("Physics layer containing objects that can be picked up.")]
    public LayerMask PickupMask;
    [Tooltip("Distance at which objects can be picked up.")]
    public float pickupRange = 5f;

    [Space]
    [Tooltip("Transform under which a picked-up object will be parented.")]
    public Transform HoldParent;

    [SerializeField, ReadOnly]
    private GameObject heldObj;


#if UNITY_EDITOR
    public bool DrawGizmos = true;
#endif

    void Awake()
    {
        if (this.HoldParent == null)
            Debug.LogError("Pickup is missing a reference to a Transform for its HoldParent.");

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!this.DrawGizmos)
            return;

        RaycastHit hit;

        Gizmos.color = Physics.Raycast(transform.position, this.transform.forward, out hit, this.pickupRange, this.PickupMask)
            ? Color.green
            : Color.red;

        Gizmos.DrawRay(transform.position, this.transform.forward * this.pickupRange);
        Gizmos.DrawRay(
            this.HoldParent.position,
            (this.transform.position + this.transform.forward * this.pickupRange) - this.HoldParent.position
            );
    }
#endif

    public void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, this.transform.forward, out hit, this.pickupRange, this.PickupMask.value))
        {
            if (this.heldObj != null)
                this.DropHeldObject();

            this.PickUpObject(hit.transform.gameObject);
        }
    }


    private void PickUpObject(GameObject target)
    {
        Rigidbody rb = target.GetComponentInChildren<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        this.heldObj = target;
        this.heldObj.transform.parent = this.HoldParent;
        this.heldObj.transform.localPosition = Vector3.zero;
    }


    public void DropHeldObject()
    {
        if (this.heldObj == null)
            return;

        Rigidbody rb = this.heldObj.GetComponentInChildren<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        this.HoldParent.DetachChildren();
        this.heldObj = null;
    }


}
