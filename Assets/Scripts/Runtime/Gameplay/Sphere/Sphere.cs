using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

[System.Serializable]
public class Sphere : MonoBehaviour, ITouchable
{
    public int Index;
    public Material Material;
    public GameObject SphereObject;
    public NavMeshAgent agent;
    private float rotationDuration = 20f; // Rotation duration in seconds
    private Vector3 rotationAxis = Vector3.up; // Rotation axis (Y-axis)
    public float rayLength = 1f;
    public string[] collisionTags;
    
    public bool canMove;
    
    // Field to store the previous position of the spheres
    public Vector3 PreviousPosition { get; private set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //RotateSphere(); // Start rotating
    }

    public void MoveToWithAgent(Vector3 targetPosition, System.Action onComplete = null)
    {
        canMove = CanMove();
        if (canMove)
        {
            if (!agent.enabled)
            {
                agent.enabled = true;
            }

            PreviousPosition = transform.position;
            agent.SetDestination(targetPosition);
            StartCoroutine(CheckIfReachedDestination(onComplete ));
        }

        if (canMove != true)
        {
            Debug.Log("buraya girdim aminakoidugunm ");
            PreviousPosition = transform.position;
            agent.enabled = false;
           
            StartCoroutine(CheckIfReachedDestination(onComplete ));
        }
    }



    // Method to move the sphere back to its previous position
    public void MoveBack(System.Action onComplete = null)
    {
        if (agent != null)
        {
            agent.SetDestination(PreviousPosition);
           
            StartCoroutine(CheckIfReachedDestination(onComplete ));
        }
    }

    private IEnumerator CheckIfReachedDestination(System.Action onComplete )
    {
       
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            onComplete?.Invoke();
        
        
        
       
    }

    private void RotateSphere()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationDuration);
    }

    private int CheckRaycastCollisions()
    {
        int tagCount = 0; // Initialize count
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        foreach (Vector3 direction in directions)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, direction * rayLength, Color.red, 1f);

            if (Physics.Raycast(transform.position, direction, out hit, rayLength))
            {
                Debug.Log("Hit detected with object: " + hit.collider.name + " with tag: " + hit.collider.tag);

                
                Debug.Log("ANNISIKEIYM "); 
                Debug.Log("Tag matches: " + hit.collider.tag ); 
                tagCount++; // Increment count when a matching tag is detected
                
            }
            else
            {
                Debug.Log("No hit detected in direction: " + direction);
            }
        }

        Debug.Log("Total tags matched: " + tagCount);
        return tagCount;
    }

    private bool IsInCollisionTags(string tag)
    {
        foreach (string collisionTag in collisionTags)
        {
            if (collisionTag == tag)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanMove()
    {
        int collisionCount = CheckRaycastCollisions();
        Debug.Log("CanMove called. Collisions detected: " + collisionCount);
        return collisionCount < 4; // Allow movement if fewer than 4 collisions are detected
    }
}
