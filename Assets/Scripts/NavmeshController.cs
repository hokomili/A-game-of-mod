using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus;
using NavMeshPlus.Components;
public class NavmeshController : MonoBehaviour
{
    NavMeshSurface Surface2D;
    float innertimer=0f;
    // Start is called before the first frame update
    void Start()
    {
        Surface2D=GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        if(innertimer>0.033){
            UpdateNavMesh();
            innertimer=0;
        }
        innertimer+=Time.deltaTime;
        
    }
    void UpdateNavMesh(){
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }
}
