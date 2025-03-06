using System;
using UnityEngine;


public class Node
{
    public enum NodeState
    {
        normal,
        test1,test2,test3
    }

    public NodeState nodeState { get;private set } = NodeState.normal;
    

}

public class NodeType1 : Node
{
    public NodeType1()
    {
        nodeState = NodeState.test1;
    }
}