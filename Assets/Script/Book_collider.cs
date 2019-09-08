using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_collider : MonoBehaviour
{
  public Animator Start;
  public Collider2D Collider;

  void OnMouseDown ()
  {
    Start.SetTrigger("Open");
    Collider.enabled = false;
  }
}
