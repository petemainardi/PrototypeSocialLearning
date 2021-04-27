using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Reticule : MonoBehaviour
{
    public PickerUpper pickerUpper;
    private Camera cam;
    
    [Space]
    public Image image;
    private Color originalColor;
    public Color interactColor;
    public LayerMask interactionLayer;

    private void Awake()
    {
        this.originalColor = image.color;
        this.cam = this.pickerUpper.GetComponent<Camera>();
    }

    private void Start()
    {
        //this.transform.localPosition = this.pickerUpper.transform.forward * this.pickerUpper.pickupRange;

        this.pickerUpper.ObserveEveryValueChanged(pu => pu.HeldObject)
            .Subscribe(g => this.image.enabled = g == null && this.cam != null && this.cam.enabled)
            .AddTo(this);

        this.cam.ObserveEveryValueChanged(c => c.enabled)
            .Subscribe(b => this.image.enabled = b && this.pickerUpper.HeldObject == null)
            .AddTo(this);
    }

    private void Update()
    {
        Color c = this.originalColor;
        RaycastHit hit;
        if (Physics.Raycast(
            this.pickerUpper.transform.position,
            this.pickerUpper.transform.forward,
            out hit,
            this.pickerUpper.pickupRange)
            && ((1 << hit.collider.gameObject.layer) & this.interactionLayer) > 0
            )
        {

            c = this.interactColor;
        }
        this.image.color = c;
    }
}
