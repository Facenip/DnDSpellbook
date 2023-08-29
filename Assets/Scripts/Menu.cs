using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private Transform menu;
    [SerializeField] TMP_InputField Name, Lvl, Class, Rase;
    public void ShowMenu()
    {
        menu.SetAsLastSibling();
    }

    public void DeleteMenu()
    {
        menu.SetAsFirstSibling();
        Name.text = "";
        Lvl.text = "";
        Class.text = "";
        Rase.text = "";
    }
}
