using System.Collections.Generic;

public class Node
{
    public enum Status
    {
        SUCCESS, FAILURE, RUNNING
    }
    public Status status;

    public List<Node> children = new List<Node>();
    public int currentChild = 0;
    public string name;

    public Node()
    {

    }
    public Node(string name)
    {
        this.name = name;
    }
    public void AddChild(Node child)
    {
        children.Add(child);
    }

    public virtual Status Process()
    {
        return children[currentChild].Process();

    }

}
