public class Leaf : Node
{
    public delegate Status Tick();
    private Tick ProcessMethod;

    public Leaf()
    {

    }
    public Leaf(string name, Tick processMethod)
    {
        this.name = name;
        this.ProcessMethod = processMethod;
    }
    public override Status Process()
    {
        if (ProcessMethod != null)
        {
            return ProcessMethod();
        }

        return Status.FAILURE;
    }
}
