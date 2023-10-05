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
    public IDbConnection dbconnection;

    private void Start()
    {
        tableConnection();
        tableReset();
    }

    public void tableConnection()
    {
        string connection;
        connection = "URI=file:" + Application.dataPath + "/MainDB.s3db";
        dbconnection = new SqliteConnection(connection);
        dbconnection.Open();
    }


    public void tableReset()
    {
        IDbCommand cmd = dbconnection.CreateCommand();
        cmd.CommandText = "SELECT * FROM Character";

        IDataReader reader = cmd.ExecuteReader();

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
