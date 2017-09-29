namespace MsgPackNetBenchmarks.Shared.Models
{
    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public int CivicNumber { get; set; }

        public override string ToString()
        {
            return $"{nameof(Address)}: {nameof(Street)}={this.Street}, {nameof(City)}={this.City}, {nameof(ZipCode)}={this.ZipCode}, {nameof(CivicNumber)}={this.CivicNumber}";
        }
    }
}
