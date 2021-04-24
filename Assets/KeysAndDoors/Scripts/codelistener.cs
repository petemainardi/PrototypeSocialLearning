using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class codelistener : MonoBehaviour
{
    public Text label;
    // Start is called before the first frame update

    public void updateText(int decode) {
        label.text = decode.ToString();
    }

}
