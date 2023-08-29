using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] waypoints;
    public Transform player;
    int waypointIndex = 0;
    Vector3 target;
    bool jugadorDentroArea;

    int colisionCon = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            colisionCon = 1;
        }
    }


    [Task]
    private void VerificarJugadorEnAreaAtaque()
    {
        jugadorDentroArea = JugadorDentroArea();
        if (jugadorDentroArea)
        {
            Task.current.Succeed();
        }
        else
        {
            UpdateDestination();
            Task.current.Fail();
        }
    }

    [Task]
    private void DispararLaser()
    {
        EnemyShot.instance.playerFound = true;
        Task.current.Succeed();
    }

    [Task]
    private void IrTrasJugador()
    {
        if(colisionCon == 1)
        {
            colisionCon = 0;
            Task.current.Succeed();
        }   
        agent.destination = player.position;
    }

    [Task]
    private void DejarDeDisparar()
    {
        EnemyShot.instance.playerFound = false;
        Task.current.Succeed();
    }


    [Task]
    private void CaminarArea()
    {
        jugadorDentroArea = JugadorDentroArea();
        if (jugadorDentroArea)
            Task.current.Fail();
        else
        {
            if (Vector3.Distance(transform.position, target) < 1.5)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }                
        }
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        jugadorDentroArea = JugadorDentroArea();

        //print("Distancia es: " + Vector3.Distance(transform.position, target));

        //OJO
        //        agent.destination = player.position;

    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.destination = target;
    }
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    //Verificar si el jugador esta dentro del area del enemigo
    
    private void JugadorEnAreaAtaque()
    {

    }


    private bool JugadorDentroArea()
    {
        Vector3 maxPoint = waypoints[0].position;
        Vector3 minPoint = waypoints[2].position;

        if (player.position.x >= minPoint.x && player.position.x <= maxPoint.x &&
            player.position.z <= minPoint.z && player.position.z >= maxPoint.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
