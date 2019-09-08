using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GlobalGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] quantity_couleur;
    public Color[] code_couleur;
    public int[] emotion_quantity;

    private char[] equivalence_color;

    public string my_value;

    void Start()
    {
      quantity_couleur = new int[6];
      code_couleur  = new Color[6];
      equivalence_color = new char[6];
      emotion_quantity = new int[6];
      equivalence_color[0] = 'j';
      equivalence_color[1] = 'a';
      equivalence_color[2] = 'f';
      equivalence_color[3] = 'l';
      equivalence_color[4] = 't';
      equivalence_color[5] = 'd';
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown("escape"))
        {
          #if UNITY_EDITOR
                  UnityEditor.EditorApplication.isPlaying = false;
          #else
                  Application.Quit ();
          #endif
        }
    }

    void FixChoice()
    {
      foreach (string color_code in my_value.Split('-'))
      {
        int index_color = ArrayUtility.IndexOf(equivalence_color, color_code[0]);
        quantity_couleur[index_color] += int.Parse(color_code);
      }
    }
}
