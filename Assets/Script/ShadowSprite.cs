using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    private SpriteRenderer thisSprote;
    private SpriteRenderer playerSprite;
    private Color color;
    [Header("时间控制")]
    public float activeTime;
    public float activeStart;

    [Header("不透明度")]
    private float alpha;
    public float alphaInit;
    public float alphaMultiplier;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprote = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        alpha = alphaInit;
        thisSprote.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1,1,1,alpha);
        thisSprote.color = color;

        if (Time.time>= activeStart+activeTime)
        {
            //返回对象池
            GameObjectPool.Instance.ReturnPool(this.gameObject);
        }
    }
}
