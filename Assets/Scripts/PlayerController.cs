using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target1;
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    static float agentDrift = 0.0001f; // minimal
    public bool destinating;
    void SetDestination(GameObject target)
    {
        target1.SetActive(true);
		if(Mathf.Abs(transform.position.x - target.transform.position.x) < agentDrift){
            var driftPos = target.transform.position + new Vector3(agentDrift, 0f, 0f);
            agent.SetDestination(driftPos);
        }
        else{
            agent.SetDestination(target.transform.position);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        agent=this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,worldPosition)<agent.stoppingDistance){
            target1.SetActive(false);
            destinating=false;
        }
        if(destinating){
            screenPosition=Mouse.current.position.ReadValue();
            screenPosition.z=Camera.main.nearClipPlane+1;
            worldPosition=Camera.main.ScreenToWorldPoint(screenPosition);
            target1.transform.position=worldPosition;
            SetDestination(target1);
        }
        
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton()){
            destinating=true;
            screenPosition=Mouse.current.position.ReadValue();
            screenPosition.z=Camera.main.nearClipPlane+1;
            worldPosition=Camera.main.ScreenToWorldPoint(screenPosition);
            target1.transform.position=worldPosition;
        }
        else{
            destinating=false;
        }
        
    }
}
