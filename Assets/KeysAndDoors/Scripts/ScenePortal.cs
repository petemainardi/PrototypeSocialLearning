using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public string SceneToLoad;
    public LayerMask DetectionMask;

    private void OnTriggerEnter(Collider other)
    {
        if ( ((1 << other.gameObject.layer) == this.DetectionMask.value
            || other.gameObject.layer == 0 && this.DetectionMask.value == 0)
            && !string.IsNullOrEmpty(this.SceneToLoad))
        {
            SceneManager.LoadScene(this.SceneToLoad);
        }
    }
}
