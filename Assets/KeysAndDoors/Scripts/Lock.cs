using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using UniRx.Triggers;

[RequireComponent(typeof(Collider))]
public class Lock : MonoBehaviour
{
    [Header("Properties")]
    private Collider col;
    public MeshRenderer mesh;
    public Color HighlightColor;
    private Color originalColor;

    [Header("Doors & Keys")]
    public KeyCode UnlockKey = KeyCode.Mouse0;
    public List<Door> Doors = new List<Door>();
    public List<Key> Keys = new List<Key>();

    [SerializeField, ReadOnly]
    private Key CollidingWith;

    [Space]
    public Vector2Int CodeRange = new Vector2Int(0, 256);
    [SerializeField] private int CurrentCode;
    public int Code => this.CurrentCode;

    public UnityEvent<int> OnGenerateCode;


    private void Awake()
    {
        if (this.mesh == null)
            Debug.LogError($"Door {this.name}:{this.GetInstanceID()} is missing its Mesh.");
        this.originalColor = this.mesh.material.color;

        this.col = this.GetComponent<Collider>();
        this.col.isTrigger = true;

    }

    private void Start()
    {
        this.GenerateNewCode();
    }

    private void Update()
    {
        if (Input.GetKeyDown(this.UnlockKey) && this.CollidingWith != null)
        {
            if (this.CollidingWith.Code == this.Code)
                this.Unlock();
            else
                this.GenerateNewCode();
            // TODO: Maybe also signal UI message telling player it was wrong?
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Key key = other.GetComponent<Key>();
        if (key != null)
        {
            this.mesh.material.color = this.HighlightColor;
            this.CollidingWith = key;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.CollidingWith != null && other.gameObject == this.CollidingWith.gameObject)
        {
            this.mesh.material.color = this.originalColor;
            this.CollidingWith = null;
        }
    }



    public void GenerateNewCode()
    {
        foreach (Key key in this.Keys)
        {
            key.AssignCode(UnityEngine.Random.Range(this.CodeRange.x, this.CodeRange.y));   //TODO: Ensure that codes are distinct
            key.ReturnToStart();
        }

        this.CurrentCode = this.Keys[UnityEngine.Random.Range(0, this.Keys.Count)].Code;
        this.OnGenerateCode.Invoke(this.CurrentCode);
    }


    public void Unlock()
    {
        foreach (Key key in this.Keys)
            key.ReturnToStart();

        foreach (Door d in this.Doors)
            d.Open();
    }


}
