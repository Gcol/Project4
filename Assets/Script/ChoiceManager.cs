using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{

    public string my_description = "";
    public TextMesh text;
    public GameObject current_barre;
    public GameObject other_barre_1;
    public GameObject other_barre_2;


    void OnMouseDown ()
    {
      current_barre.SetActive(false);
      other_barre_1.SetActive(true);
      other_barre_2.SetActive(true);
      text.text = my_description;
    }
}
