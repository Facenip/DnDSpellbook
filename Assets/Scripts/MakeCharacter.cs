using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeCharacter : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform parent;
    [SerializeField] TMP_InputField Name, LvL, Class, Rase;
    public void NewCharacter()
    {
        character.transform.Find("CharacterName").GetComponent<TextMeshProUGUI>().text=Name.text;
        character.transform.Find("LvL").GetComponent<TextMeshProUGUI>().text = "��: "+LvL.text;
        character.transform.Find("CharacterClass").GetComponent<TextMeshProUGUI>().text = "�����: " + Class.text;
        character.transform.Find("CharacterRase").GetComponent<TextMeshProUGUI>().text = "�����: " + Rase.text;
        Instantiate(character, parent);
    }
}
