using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    public void LoadScene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }
}
