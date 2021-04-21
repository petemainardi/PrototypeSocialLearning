using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 closedPosition;
    public Vector3 OpenOffset = new Vector3(0, 5, 0);

    private void Awake()
    {
        this.closedPosition = this.transform.position;
    }

    public void Open() => StartCoroutine(this.MoveToOpenPos());

    private IEnumerator MoveToOpenPos()
    {
        while ((float)Math.Abs((this.transform.position -  (this.closedPosition + this.OpenOffset)).sqrMagnitude) > 0.1f)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, this.closedPosition + OpenOffset, Time.deltaTime);
            yield return null;
        }
    }
}
