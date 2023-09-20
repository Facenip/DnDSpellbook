using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using TMPro;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform parent;
    public SqliteConnection dbconnection;
    private string path;

    private void Start()
    {
        tableConnection();
        tableReset();
    }

    public void tableConnection()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
           path = Application.dataPath + "/StreamingAssets/maindb.bytes";
        }
        else
        {
            path = Application.persistentDataPath + "/StreamingAssets/maindb.bytes";
        }

        dbconnection = new SqliteConnection("URI=file:" + path);
        dbconnection.Open();
    }

    public void tableReset()
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT * FROM Character";

        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            character.transform.Find("CharacterName").GetComponent<TextMeshProUGUI>().text = (string)reader[1];
            character.transform.Find("LvL").GetComponent<TextMeshProUGUI>().text = "Ур: " + (string)reader[4];
            character.transform.Find("CharacterClass").GetComponent<TextMeshProUGUI>().text = "Класс: " + (string)reader[2];
            character.transform.Find("CharacterRase").GetComponent<TextMeshProUGUI>().text = "Расса: " + (string)reader[3];
            Instantiate(character, parent);
        }
    }
}
