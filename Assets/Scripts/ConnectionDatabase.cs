using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using System.IO;

public class ConnectionDatabase : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform parent;
    [SerializeField] TMP_InputField Name, LvL, Class, Rase;
    public SqliteConnection dbconnection;
    private string DBPath;
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

        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }

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


    public void newCharacter()
    {
        SqliteCommand cmdWrite = dbconnection.CreateCommand();
        cmdWrite.CommandText = "INSERT INTO Character (Name,Class,Rase,lvl) VALUES (\""+ Name.text + "\",\"" + Class.text + "\",\"" + Rase.text + "\",\"" + LvL.text + "\")";
        cmdWrite.ExecuteNonQuery();
        tableReset();
    }
}
