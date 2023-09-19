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
        path = Application.dataPath + "/DataBase/maindb.bytes";
        dbconnection = new SqliteConnection("URI=file:" + path);
        dbconnection.Open();
/*        if(dbconnection.State == ConnectionState.Open)
        {
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbconnection;
            cmd.CommandText = "SELECT * FROM Character";

            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Debug.Log(String.Format("{0}  {1}  {2}  {3}  {4}", reader[0],reader[1], reader[2], reader[3], reader[4]));
            }
        }
        else
        {
            Debug.Log("Error connection");
        }*/
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
