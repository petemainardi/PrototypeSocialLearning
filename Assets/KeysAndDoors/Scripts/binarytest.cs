using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class binarytest : MonoBehaviour
{
    
    public shelf zero;
    public shelf one;
    public shelf two;
    public shelf three;
    public shelf four;
    public shelf five;
    public shelf six;
    public shelf seven;
    public int sum;
    [Space]
    public Text label;
    public bool DisplayAsBinary = false;

    // Update is called once per frame
    void Update()
    {
        sum = zero.output + one.output + two.output + three.output + four.output + five.output + six.output + seven.output;
        label.text = !this.DisplayAsBinary ? sum.ToString() : Convert.ToString(sum, 2).PadLeft(8, '0');
    }
}
