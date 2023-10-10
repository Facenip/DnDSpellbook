using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using System.IO;

public class Connection : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform parent;
    public SqliteConnection dbconnection;
    public string DBPath;
    private string fileName = "db.bytes";

    private void Start()
    {
        tableConnection();
        tableReset();
    }


    private string GetDatabasePath()
    {
#if UNITY_EDITOR
        return Path.Combine(Application.streamingAssetsPath, fileName);
#endif
#if UNITY_STANDALONE
    string filePath = Path.Combine(Application.dataPath, fileName);
    if(!File.Exists(filePath)) UnpackDatabase(filePath);
    return filePath;
#elif UNITY_ANDROID
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(filePath)) UnpackDatabase(filePath);
        return filePath;
#endif
    }

    private void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.streamingAssetsPath, fileName);

        WWW reader = new WWW(fromPath);
        while (!reader.isDone) { }

        File.WriteAllBytes(toPath, reader.bytes);
    }


    public void tableConnection()
    {
        DBPath = GetDatabasePath();
        dbconnection = new SqliteConnection("Data Source=" + DBPath);
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
            character.transform.Find("LvL").GetComponent<TextMeshProUGUI>().text = "��: " + (string)reader[4];
            character.transform.Find("CharacterClass").GetComponent<TextMeshProUGUI>().text = "�����: " + (string)reader[2];
            character.transform.Find("CharacterRase").GetComponent<TextMeshProUGUI>().text = "�����: " + (string)reader[3];
            Instantiate(character, parent);
        }
    }
}
