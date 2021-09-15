using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public void GetItem() {
        FindObjectOfType<PlayerController>().GetItemCount();
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    
}
