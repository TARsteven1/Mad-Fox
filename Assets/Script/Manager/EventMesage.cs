using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventMesage : MonoBehaviour
{
    public GameObject enterDialog;
    private bool changeScene;
    //public GameObject Canvs;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            changeScene = true;
            enterDialog.SetActive(changeScene);
            Invoke("ChangeScene", 2f);
           
        }
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
        //    //DontDestroyOnLoad(this.gameObject);
        //    //加载下一个编号的场景
        //    // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            changeScene = false;
            enterDialog.SetActive(changeScene);
        }
    }
    private void ChangeScene() {
        if (changeScene)
        {
            SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
        }
       
    }

}
