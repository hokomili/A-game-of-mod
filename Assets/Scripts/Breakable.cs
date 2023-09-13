using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
public class Breakable : MonoBehaviour
{
    public float hp=3;
    public float cooldown=1.0f;
    float innertimer=0;
    public GameObject text;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(innertimer>=0){
            innertimer-=Time.deltaTime;
        }
    }
    public void Hit(float damage,GameObject source){
        if(innertimer<0){
            hp-=damage;
            innertimer=cooldown;
            GameObject newtext=Instantiate(text);
            newtext.transform.SetParent(canvas.transform);
            Vector2 ViewportPosition=Camera.main.WorldToViewportPoint(transform.position);
            RectTransform CanvasRect=canvas.GetComponent<RectTransform>();
            Vector2 WorldObject_ScreenPosition=new Vector2(
            ((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
            ((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));
            newtext.GetComponent<RectTransform>().anchoredPosition=WorldObject_ScreenPosition;
            Debug.Log(source.name+"dealt"+damage+"damage to"+gameObject.name);
            if(hp<=0){
                gameObject.SetActive(false);
            }
        }
    }
}
