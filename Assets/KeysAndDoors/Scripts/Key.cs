using System;
using UnityEngine;
using UniRx;

public class Key : MonoBehaviour
{
    [SerializeField, ReadOnly] private Transform originalParent;
    [SerializeField, ReadOnly] private Vector3 originalPosition;
    [SerializeField, ReadOnly] private Quaternion originalRotation;
    [SerializeField, ReadOnly] private Vector3 originalScale;

    private Rigidbody rb;

    [Space]
    public CodeLabel label;

    [SerializeField, ReadOnly]
    private int code;
    public int Code => this.code;

    private void Awake()
    {
        this.originalParent = this.transform.parent;
        this.originalPosition = transform.localPosition;
        this.originalRotation = transform.localRotation;
        this.originalScale = transform.localScale;

        this.rb = this.GetComponentInChildren<Rigidbody>();
        if (this.rb != null)
            this.rb.isKinematic = true;

        if (this.label == null)
            Debug.LogError($"Key {this.name}:{this.GetInstanceID()} is missing its CodeLabel.");
        this.label.Follow(this.transform);

        this.ObserveEveryValueChanged(k => k.transform.parent)
            .Subscribe(p =>
            {
                if (p != this.originalParent)
                {
                    this.label.Face(Camera.main.transform);
                    if (p != null)
                    {
                        this.transform.localRotation = Quaternion.Euler(-90, 100, -90);
                    }
                }
            })
            .AddTo(this);
    }


    public void ReturnToStart()
    {
        //this.transform.parent = null;
        this.transform.parent = this.originalParent;
        this.transform.localPosition = this.originalPosition;
        this.transform.localRotation = this.originalRotation;
        this.transform.localScale = this.originalScale;

        if (this.rb != null)
            this.rb.isKinematic = true;

        this.label.Face(null);
    }

    public void AssignCode(int n)
    {
        this.code = n;
        this.label.SetText(n.ToString()); // Should keys be binary or transmitted code be binary?
    }

}
