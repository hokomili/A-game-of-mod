using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour
{
    public float duration=1.0f;
    float innertimer=0f;
    // Start is called before the first frame update
    void Start()
    {
        innertimer=duration;
    }

    // Update is called once per frame
    void Update()
    {
        if(innertimer>=0f){
            innertimer-=Time.deltaTime;
        }
        else{
            Destroy(gameObject);
        }
    }
}
