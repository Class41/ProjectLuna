using UnityEngine;
using UnityEngine.UI;

public class UI_namegen : MonoBehaviour
{
    string[] _first = { "Vel", "Xil", "Hekir", "Zuun", "Lih", "Juil", "Bikir"};
    string[] _middle = { "Shul", "Ska", "Jah", "Jas", "Skun", "Kur", "Gur"};
    string[] _last = { "of Ashes", "of Fury", "of Rage", "of Anger", "the Soulless", "the Mad", "the Mindless"};

    Text _name;
    // Start is called before the first frame update
    void Start()
    {
        _name = gameObject.GetComponent<Text>();

        _name.text += _first[Random.Range(0, _first.Length - 1)] + "'";
        _name.text += _middle[Random.Range(0, _middle.Length - 1)] + " ";
        _name.text += _last[Random.Range(0, _last.Length - 1)];
    }
}
