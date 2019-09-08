using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPage : MonoBehaviour
{
  public TextManager my_book;

  void OnMouseDown ()
  {
    my_book.New_Page();
  }
}
