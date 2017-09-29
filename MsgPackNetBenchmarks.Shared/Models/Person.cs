namespace MsgPackNetBenchmarks.Shared.Models
{
    public class Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Address Address { get; set; }

        public override string ToString()
        {
            return $"{nameof(Person)}: {nameof(Name)}={this.Name}, {nameof(Surname)}={this.Surname}, {nameof(Age)}={this.Age}, {nameof(Address)}={this.Address}";
        }
    }
}
