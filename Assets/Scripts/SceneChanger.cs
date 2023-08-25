using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    int sceneid = 0;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneid);
    }
}
