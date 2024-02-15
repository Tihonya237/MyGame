using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string SceneToLoad;
    public Vector2 position;
    public VectorValue PlayerStorage;

    public void Switch()
    {
        PlayerStorage.initialValue = position;
        SceneManager.LoadScene(SceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerStorage.initialValue = position;
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
