using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent; 
    // Update is called once per frame
    void Update()
    {
        // Descomentar si quieren jugar con el pollo y que se mueva haciendo clicks en pantalla 
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //
        //     if (Physics.Raycast(ray, out hit))
        //         agent.SetDestination(hit.point); 
        // }
        
        // Otra manera de testearlo: random walk. Le pongo un destino y cuando llego salí para otro aleatorio.
        if(agent.remainingDistance < agent.stoppingDistance)
            agent.SetDestination(new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5))); 
        
    }
}
