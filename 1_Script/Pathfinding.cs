using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {

    public Transform seeker, target;
    public Transform DestPoint;
    private Rigidbody seekerRigid;
    private Grid grid;
    private Node Destination;
    private Transform[] ArrayDestination;
    private Vector3 vCurrentPosition;
    private CBossManager pBossManager;
    private float PlayerDistance = 0;
    public float GetPlayerDistance { get { return PlayerDistance; } }
    private Animator MonsterAnim;
    private float fTotalDistance;
    public float GetTotalDistance {  get { return fTotalDistance; } }
    public bool bIsAStar = false;
    private bool bIsAni = true;


    Node[] pTempNode = new Node[2];
    public Node[] GetTempNode
    {
        get { return pTempNode; }
    }
    [SerializeField] private float fSpeed = 0;
    public float _fspeed { get { return fSpeed; } set { fSpeed = value; } }
    [SerializeField] float StartTime = 0;
    private void Awake()
    {
        grid = GetComponent<Grid>();
        seekerRigid = seeker.GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pBossManager = seeker.GetComponent<CBossManager>();
    }

    private void Start()
    {
       
    }

    public void MoveToTarget(Node pTarget)
    {
        Destination = pTarget;
      
    }
   
    public void FindPathWithPriorityQueue(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
      
        Node startNode = grid.NodeFromWorldPosition(startPos);
        Node targetNode = grid.NodeFromWorldPosition(targetPos);
        pTempNode[1] = grid.NodeFromWorldPosition(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
        pTempNode[0] = grid.NodeFromWorldPosition(targetPos);
        PriorityQueue<Node> openSet = new PriorityQueue<Node>(1000);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Enqueue(startNode);
        fTotalDistance = GetDistance(startNode, targetNode);
        while (openSet.Count() > 0)
        {
            Node currentNode = openSet.Dequeue();
            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
             
                sw.Stop();
                return;
            }
          
            closedSet.Add(currentNode);

            foreach (Node n in grid.GetNeighbours(currentNode))
            {
                if (n.walkable || closedSet.Contains(n))
                    continue;

                int g = currentNode.gCost + GetDistance(currentNode, n);
                int h = GetDistance(n, targetNode);
               
                int f = g + h;
                
                if (!openSet.Contains(n))
                {
                    n.gCost = g;
                    n.hCost = h;
                    n.parent = currentNode;
                    openSet.Enqueue(n);
                }
                else
                {
                    if (n.fCost > f || (n.fCost == f && n.gCost > g))
                    {
                        n.gCost = g;
                        n.parent = currentNode;
                    }
                }
            }

        }
       
    }
    // For RenderGrid
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        grid.path = path;
    }

   public  int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }

        return 14 * dstX + 10 * (dstY - dstX);
    }
}
