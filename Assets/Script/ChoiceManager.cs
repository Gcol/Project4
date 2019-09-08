using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{

    public string my_text;
    public string quantity;
    public TextMesh text;
    public GameObject current_barre;
    public GameObject other_barre_1;
    public GameObject other_barre_2;

    public GlobalGameManager global;

    void OnMouseDown ()
    {
      current_barre.SetActive(false);
      other_barre_1.SetActive(true);
      other_barre_2.SetActive(true);
      text.text = my_text;
      global.my_value = quantity;
    }
}
