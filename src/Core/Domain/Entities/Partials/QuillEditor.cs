namespace Domain.Entities.Partials;

public class Attributes
{
    public string list { get; set; }
}

public class Op
{
    public string insert { get; set; }
    public Attributes? attributes { get; set; }
}

public class QuillEditor
{
    public QuillEditor()
    {
        ops = [];
    }

    public List<Op> ops { get; set; }
}
