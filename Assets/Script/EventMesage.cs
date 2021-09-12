using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventMesage : MonoBehaviour
{
    public GameObject enterDialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            enterDialog.SetActive(true);
        }
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Scene2",LoadSceneMode.Single);
            //加载下一个编号的场景
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialog.SetActive(false);
        }
    }

}
