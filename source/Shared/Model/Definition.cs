namespace Shared.Model
{
    public class Definition
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
