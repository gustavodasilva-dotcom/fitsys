namespace Web.Models.Entities;

public class Attributes
{
    public string list { get; set; }
}

public class Op
{
    public string insert { get; set; }
    public Attributes? attributes { get; set; }
}

public class QuillEditorInputModel
{
    public List<Op> ops { get; set; }
}
