using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using Mono.Data;
using TMPro;
using System.IO;

public class Connection : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform parent;
    public SqliteConnection dbconnection;

    private void Start()
    {
        tableConnection();
        tableReset();
    }

    public void tableConnection()
    {
        string connection;
        if (Application.platform != RuntimePlatform.Android)
        {
            connection = Application.dataPath + "/DataBase.db";
        }
        else
        {
            connection = Application.persistentDataPath + "/DataBase.db";
            if (!File.Exists(connection))
            {
                WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "DataBase.db");
                while (!load.isDone) { }

                File.WriteAllBytes(connection, load.bytes);
            }
        }
        dbconnection = new SqliteConnection("URI=file:" + connection);
        dbconnection.Open();
    }


    public void tableReset()
    {
        SqliteCommand cmd = dbconnection.CreateCommand();
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
