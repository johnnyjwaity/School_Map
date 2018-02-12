﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {


    public List<Node> parentNodes = new List<Node>();
    public List<Node> childrenNodes = new List<Node>();
    public int nodeId;
    public bool isStartNode;
    public bool isFinishNode;
    public bool search;
    private Node finish;
    public bool floor1Node;

    private List<List<Node>> currentPath = new List<List<Node>>();

	// Use this for initialization
	void Start () {
        if(isStartNode){
            
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (search)
        {
            scan();
            Debug.Log("Finished Scanning");
            currentPath = foundFinish();
            Debug.Log(currentPath.Count);
            search = false;
        }

        List <Color> lineColors = new List<Color>();
        lineColors.Add(Color.red);
        lineColors.Add(Color.yellow);
        lineColors.Add(Color.green);
        lineColors.Add(Color.blue);
        lineColors.Add(Color.black);
        lineColors.Add(Color.gray);
        int counter = 0;

        foreach(List<Node> path in currentPath){
            for (int i = 0; i < path.Count - 1; i++){
                Debug.DrawLine(path[i].transform.position, path[i+1].transform.position, lineColors[counter], 1, true);
            }
            counter++;
        }


	}


    public void scan(){
        Node[] allNodesTemp = FindObjectsOfType<Node>();
        List<Node> allNodes = new List<Node>();
        
        foreach(Node aNode in allNodesTemp)
        {
            if(aNode.floor1Node == floor1Node)
            {
                allNodes.Add(aNode);
            }
        }
        Debug.Log("Found All Same Floor Nodes");
        
        //Debug.Log("All Nodes Length" + allNodes.Length);
        List<Node> sightedNodes = new List<Node>();
        foreach(Node cNode in allNodes){
            if (cNode.isFinishNode)
            {
                finish = cNode;
            }
            bool isParent = false;
            foreach(Node pNode in parentNodes){
                if(cNode.nodeId == pNode.nodeId){
                    isParent = true;
                    break;
                }
            }
            if (isParent || cNode.nodeId == nodeId){
                continue;
            }
            //if(cNode.nodeId == nodeId){
            //    continue;
            //}
            Vector3 rayDirection = cNode.transform.position - transform.position;
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(transform.position, rayDirection.normalized, out hit)){
                if(hit.transform.gameObject == cNode.gameObject)
                {
                    //Debug.Log("Found Node In Sight", cNode);
                    sightedNodes.Add(cNode);
                    childrenNodes.Add(cNode);
                    List<Node> newParents = new List<Node>();
                    foreach(Node pNode in parentNodes)
                    {
                        newParents.Add(pNode);
                    }
                    newParents.Add(this);
                    cNode.parentNodes = newParents;
                }
            }
            
        }

        List<Node> orderedNodes = new List<Node>();
        if(finish != null)
        {
            foreach(Node sNode in sightedNodes)
            {
                bool inserted = false;
                for(int i = 0; i < orderedNodes.Count; i++)
                {
                    if(Vector3.Distance(sNode.gameObject.transform.position, finish.gameObject.transform.position) < Vector3.Distance(orderedNodes[i].gameObject.transform.position, finish.gameObject.transform.position))
                    {
                        orderedNodes.Insert(i, sNode);
                        inserted = true;
                        break;
                    }
                }
                if(!inserted)
                {
                    orderedNodes.Add(sNode);
                }
            }
        }
        else
        {
            orderedNodes = sightedNodes;
        }
        foreach(Node oNode in orderedNodes){
            Debug.DrawLine(transform.position, oNode.transform.position);
            oNode.scan();
        }
    }


    public List<List<Node>> foundFinish()
    {
        List<List<Node>> paths = new List<List<Node>>();
        foreach (Node cNode in childrenNodes)
        {
            List<Node> path = new List<Node>();
            Debug.Log("Scanning Node: ", this);
            if (cNode.isFinishNode)
            {
                path.Add(cNode);
                path.Insert(0, this);

                paths.Add(path);
            }
            else
            {
                List<List<Node>> tempPaths = cNode.foundFinish();
                foreach(List<Node> tempPath in tempPaths)
                {
                    
                    if (tempPath.Count > 0)
                    {
                        tempPath.Insert(0, this);
                        paths.Add(tempPath);
                    }
                }
                
            }
        }




        return paths;
    }




    //public List<Node> foundFinish()
    //{
    //    List<Node> path = new List<Node>();
    //    foreach(Node cNode in childrenNodes)
    //    {
    //        Debug.Log("Scanning Node: ", this);
    //        if (cNode.isFinishNode)
    //        {
    //            path.Add(cNode);
    //            path.Insert(0, this);
                
    //            return path;
    //        }
    //        List<Node> tempPath = cNode.foundFinish();
    //        if(tempPath.Count > 0)
    //        {
    //            tempPath.Insert(0, this);
    //            return tempPath;
    //        }


    //    }




    //    return path;
    //}


}
