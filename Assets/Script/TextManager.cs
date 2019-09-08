using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    public TextAsset  event_positif;
    public TextAsset  event_negatif;

    public TextMesh date_du_jour;
    private int year = 2018;
    private int day = 0;
    private int month = 1;

    public TextMesh choice_1_text;
    public TextMesh choice_2_text;
    public TextMesh choice_3_text;

    public TextMesh event_desc;
    public TextMesh resolution_desc;

    public ChoiceManager choice_1;
    public ChoiceManager choice_2;
    public ChoiceManager choice_3;

    public Event[] negatif_event_of_this_run;
    public List<Event> event_negatif_list;
    public List<Event> event_positif_list;
    private int index_event = -1;
    public  int nb_event_by_game = 5;

    public GameObject barre1;
    public GameObject barre2;
    public GameObject barre3;

    public GameObject canvas_book;
    public Animator animation_book;
    public Animator animation_camera;

    void Create_date()
    {
      int add_day = Random.Range(20, 60);

      day += add_day;

      if (day > 28)
      {
        month += 1;
      }

      if (month > 12)
      {
        year += 1;
      }

      month %= 13;
      day %= 28;
      day += 1;

      string[] correspondace_mois = new string[12] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

      string current_data = day.ToString() + " " + correspondace_mois[month].ToString() + " " + year.ToString();
      date_du_jour.text = current_data;
    }

    public void  New_Page()
    {
      index_event += 1;

      if (index_event < nb_event_by_game)
      {
        resolution_desc.text = "";
        Create_date();
        Reset_barre();
        Write_event();
      }
      else{
        animation_book.SetTrigger("Open");
        animation_camera.SetTrigger("Move_Camera");
        canvas_book.SetActive(false);
      }
    }


    // Start is called before the first frame update
    void Start()
    {
      init_positif_env();
      init_event_negatif_list();
      init_choice_list();
      New_Page();
    }


    string add_line_in_texte(string texte)
    {
      var parts = split_line(texte, 30);
      return string.Join("\n", parts);
    }

    IEnumerable<string>  split_line(string current_text, int partLength)
    {
      for (var i = 0; i < current_text.Length; i += partLength)
      yield return current_text.Substring(i, Mathf.Min(partLength, current_text.Length - i));
    }

    void init_positif_env()
    {
      event_positif_list = new List<Event>();
      string[] records = event_positif.text.Split ('\n');

      bool Header = true;
      foreach (string record in records)
      {
          string[] fields = record.Split('$');

          int index_fields = 0;
          string current_value = "";
          string current_text = "";

          if (Header == false && record != "")
          {
            foreach(string field in fields)
            {
              if (index_fields == 0)
              {
                current_value = field;
              }
              else if (index_fields == 1)
              {
                current_text = field;
              }
              index_fields += 1;
            }
            current_text = add_line_in_texte(current_text);
            Event current_positif_event = new Event(current_text, current_value);
            event_positif_list.Add(current_positif_event);
          }
          else
          {
            Header = false;
          }
      }
    }

    void init_event_negatif_list()
    {
      event_negatif_list = new List<Event>();
      bool Header = true;

      string[] records = event_negatif.text.Split ('\n');
      foreach (string record in records)
      {
        string[] fields = record.Split('$');
        int index_fields = 0;

        if ( Header ==  false && record != "")
        {

          Event[] current_positive_event = new Event[3];
          string current_text = "";

          foreach(string field in fields)
          {
            if (index_fields == 1)
            {
              int index_currection_positive = 0;
              foreach (string index_event in field.Split('-'))
              {
                int index = int.Parse(index_event) - 1;
                current_positive_event[index_currection_positive] = event_positif_list[index];
                index_currection_positive += 1;
              }

            }
            else if (index_fields == 2)
            {
              current_text = field;
            }
            index_fields += 1;
          }
          current_text = add_line_in_texte(current_text);
          Event current_negatif_event = new Event(current_text, current_positive_event);
          event_negatif_list.Add(current_negatif_event);
        }
        else{
          Header = false;
        }
      }
    }

    void init_choice_list()
    {
      int index_choice_event = 0;
      int nb_event = event_negatif_list.Count;
      List<int> choice_list = new List<int>();
      Random.InitState((int)System.DateTime.Now.Ticks );

      negatif_event_of_this_run = new Event[nb_event_by_game];
      for (int i = 0; i < nb_event_by_game; i++)
      {
        int choice =  Random.Range(0, nb_event);
        while (choice_list.Contains(choice))
        {
          choice =   Random.Range(0, nb_event);
        }
        negatif_event_of_this_run[index_choice_event] = event_negatif_list[choice];
        index_choice_event += 1;
        choice_list.Add(choice);
      }
    }

    // Update is called once per frame
    void Reset_barre()
    {
      barre1.SetActive(false);
      barre2.SetActive(false);
      barre3.SetActive(false);
    }

    void Write_event()
    {
      event_desc.text = negatif_event_of_this_run[index_event].text;
      choice_1.my_text = negatif_event_of_this_run[index_event].solution[0].text;
      choice_2.my_text = negatif_event_of_this_run[index_event].solution[1].text;
      choice_3.my_text = negatif_event_of_this_run[index_event].solution[2].text;

      choice_1.quantity = negatif_event_of_this_run[index_event].solution[2].value;
      choice_2.quantity = negatif_event_of_this_run[index_event].solution[2].value;
      choice_3.quantity = negatif_event_of_this_run[index_event].solution[2].value;

      choice_1_text.text = negatif_event_of_this_run[index_event].solution[0].text.Split('\n')[0];
      choice_2_text.text = negatif_event_of_this_run[index_event].solution[1].text.Split('\n')[0];
      choice_3_text.text = negatif_event_of_this_run[index_event].solution[2].text.Split('\n')[0];
    }

    [System.Serializable]
    public class Event
    {
        public string text;
        public Event[] solution;
        public string value;

        public Event(string current_text, Event[] current_solution)
        {
          text = current_text;
          solution = current_solution;
        }

        public Event(string current_text, string current_value)
        {
          text = current_text;
          value = current_value;
        }

    }
}
