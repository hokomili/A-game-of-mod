using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class PickaxeController : MonoBehaviour
{
    public Animation swingAnimation;
    public float swingTime=1.0f;
    public bool Swinging=false;
    float innertimer=0f;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(innertimer>=0){
            innertimer-=Time.deltaTime;
            if(innertimer<0){
                swingAnimation.Stop();
                if(Swinging){
                    playerController.SwingAgain();
                }
                else{
                    gameObject.SetActive(false);
                }
            }
        }
    }
    public void Swing(){
        innertimer=swingTime;
        foreach (AnimationState item in swingAnimation)
        {
            item.speed=1/swingTime;
        }
        swingAnimation.Play();
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<Breakable>()){
            collider.GetComponent<Breakable>().Hit(1,gameObject);
        }
    }
}
