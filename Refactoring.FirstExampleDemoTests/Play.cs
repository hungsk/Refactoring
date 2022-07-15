namespace Refactoring.FirstExampleDemoTests
{
    public class Play
    {
        public string Type { get; }
        public string Name { get; }

        public Play(string name, string type)
        {
            Name = name;
            Type = type;
        }
        
        public Play Clone()
        {
            return (Play) this.MemberwiseClone();
        }
    }
}