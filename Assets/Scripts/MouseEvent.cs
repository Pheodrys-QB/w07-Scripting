using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    Renderer ren;
    private void Start() {
        ren = GetComponent<Renderer>();

    }
    private void OnMouseOver() {
        ren.material.color = Color.green;
    }
    private void OnMouseExit() {
        ren.material.color = Color.white;
    }

}
