using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Collider))]
public class shelf : MonoBehaviour
{
    [Header("Properties")]
    private Collider col;
    public MeshRenderer mesh;
    public Text value;
    public Color HighlightColor;
    private Color originalColor;
    public int bit;
    public int io;
    public int output;

    //[Header("Doors & Keys")]
    //public KeyCode UnlockKey = KeyCode.Mouse0;
    //public List<Door> Doors = new List<Door>();
    //public List<Key> Keys = new List<Key>();

    //[SerializeField, ReadOnly]
    //private Key CollidingWith;
    // Start is called before the first frame update
    /*
    private void Awake()
    {
        if (this.mesh == null)
            Debug.LogError($"shelf {this.name}:{this.GetInstanceID()} is missing its Mesh.");
        this.originalColor = this.mesh.material.color;

        this.col = this.GetComponent<Collider>();
        this.col.isTrigger = true;

    }*/
    void Start()
    {
        this.originalColor = this.mesh.material.color;
        io = 0;
        output = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.transform != null) {
                    Clicked(hit.transform.gameObject);
                }
            }
        }
    }

    void Clicked(GameObject go)
    {
        if (go == this.gameObject)
        {
            //print(go.name);
            if (io == 0)
            {
                io = 1;
                output = (int)Math.Pow(2, bit) * io;
                this.mesh.material.color = this.HighlightColor;
                this.value.text = "1";
            }
            else if (io == 1)
            {
                io = 0;
                output = (int)Math.Pow(2, bit) * io;
                this.mesh.material.color = this.originalColor;
                this.value.text = "0";
            }
            
        }
        
    }
}
