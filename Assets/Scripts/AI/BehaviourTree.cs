public class BahaviourTree : Node
{
    public BahaviourTree()
    {
        name = "Root";
    }
    public BahaviourTree(string name)
    {
        this.name = name;
    }
    public override Status Process()
    {
        return children[currentChild].Process();
    }

}