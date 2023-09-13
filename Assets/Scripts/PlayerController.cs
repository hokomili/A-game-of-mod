using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
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
    public GameObject pickaxe;
    static float agentDrift = 0.0001f; // minimal
    public bool destinating;
    public Vector2 movement;
    public float speed;
    public Rigidbody2D rb;
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
        rb=GetComponent<Rigidbody2D>();
        agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition=Mouse.current.position.ReadValue();
        screenPosition.z=Camera.main.nearClipPlane;
        worldPosition=Camera.main.ScreenToWorldPoint(screenPosition);
        if(destinating){
            target1.transform.position=worldPosition;
            SetDestination(target1);
        }
        if(Vector2.Distance(transform.position,target1.transform.position)<agent.stoppingDistance){
            target1.SetActive(false);
            agent.isStopped=true;
            destinating=false;
        }
        if(Vector2.Distance(worldPosition,transform.position)>0.1){
            Vector2 worldvector2=worldPosition;
            Vector2 posvector2=transform.position;
            transform.up =  posvector2-worldvector2;
        }
    }
    void FixedUpdate(){
        if(movement.normalized==Vector2.up||movement.normalized==Vector2.down){
            movement.x+=0.001f;
        }
        rb.AddForce(movement*speed);
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton()){
            destinating=true;
            agent.isStopped=false;
        }
        else{
            destinating=false;
        }
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton()){
            target1.SetActive(false);
            agent.isStopped=true;
            destinating=false;
            pickaxe.SetActive(true);
            pickaxe.GetComponent<PickaxeController>().Swing();
            pickaxe.GetComponent<PickaxeController>().Swinging=true;
        }
        else{
            pickaxe.GetComponent<PickaxeController>().Swinging=false;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movement=context.ReadValue<Vector2>();
        if(movement.magnitude>0.1){
            
            target1.SetActive(false);
            agent.isStopped=true;
            destinating=false;
        }
        else{
            movement=Vector2.zero;
        }
    }
    public void SwingAgain(){
        pickaxe.SetActive(false);
        pickaxe.SetActive(true);
        pickaxe.GetComponent<PickaxeController>().Swing();
    }
}
