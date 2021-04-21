using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 6f;

    public MouseLook camController;
    private CharacterController charController;

    [Space]
    public PickerUpper pickerUpper;
    public KeyCode PickupKey = KeyCode.Mouse0;
    public KeyCode DropKey = KeyCode.Mouse1;

    [Space]
    public bool StartActive = true;
    public KeyCode SwitchPlayerKey = KeyCode.Tab;
    private List<PlayerController> otherPlayers = new List<PlayerController>();

    void Awake()
    {
        this.charController = this.GetComponent<CharacterController>();

        if (this.camController == null)
            Debug.LogError("PlayerController unable to find MouseLook component in children.");
        this.camController.playerBody = this.transform;

        if (this.pickerUpper == null)
            Debug.LogError("PlayerController unable to find Pickup component in children.");

        this.otherPlayers = GameObject.FindObjectsOfType<PlayerController>().ToList();
        this.otherPlayers.Remove(this);
        this.Activate(this.StartActive);
    }

    void Update()
    {
        this.camController.Look();
        this.Move();

        if (Input.GetKeyDown(this.PickupKey))
            this.pickerUpper.TryPickupObject();
        else if (Input.GetKeyDown(this.DropKey))
            this.pickerUpper.DropHeldObject();

        if (Input.GetKeyDown(this.SwitchPlayerKey))
        {
            this.otherPlayers.FirstOrDefault()?.Activate(true);
            this.Activate(false);
        }
    }

    private void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement = this.charController.transform.TransformDirection(movement);
        this.charController.Move(movement.normalized * this.MoveSpeed * Time.deltaTime);
    }

    public void Activate(bool active)
    {
        this.transform.Find("Mesh").gameObject.SetActive(!active);
        this.camController.GetComponent<Camera>().enabled = active;
        this.camController.GetComponent<AudioListener>().enabled = active;
        this.camController.enabled = active;
        this.pickerUpper.enabled = active;
        this.enabled = active;
    }
}
