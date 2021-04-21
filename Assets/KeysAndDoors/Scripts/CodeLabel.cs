using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CodeLabel : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public Vector3 Offset = new Vector3(0, 0.5f, 0);

    public Transform FaceTowards;
    private Quaternion originalRotation;

    private void Awake()
    {
        if (this.text == null)
            Debug.LogError($"CodeLabel {this.name}:{this.GetInstanceID()} is missing its Text.");

        this.originalRotation = this.transform.localRotation;
    }

    private void Update()
    {
        if (this.FaceTowards != null)
        {
            this.transform.LookAt(this.FaceTowards.position);
        }
    }

    public void SetText(string text) => this.text.text = text;

    public void Face(Transform obj)
    {
        this.FaceTowards = obj;
        if (obj == null)
            this.transform.localRotation = this.originalRotation;
    }
    public void Follow(Transform obj)
    {
        obj.ObserveEveryValueChanged(t => t.position)
            .Subscribe(p => this.transform.position = p + this.Offset)
            .AddTo(this);
    }

}
