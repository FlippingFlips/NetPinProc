using NetPinProc.Domain.Pdb;
using Xunit;

namespace NetPinProc.Tests.Misc
{
    public class PdbAddressConfigTests
    {
        [Theory]
        [InlineData("A0:R0-B1-G2")]
        public void DecodePdb_LED_AddressTests(string address)
        {
            var decodedAddress = PDBFunctions.DecodePdbAddress(address);
            Assert.True(decodedAddress.Board == 0);
        }

        [Theory]
        [InlineData("0/0/0")]//Board 0, bankA (0) , Output 0
        [InlineData("0/1/0")]//Board 0, bankB (1) , Output 0
        public void DecodePdb_COIL_AddressTests(string address)
        {
            var decodedAddress = PDBFunctions.DecodePdbAddress(address);
            Assert.True(decodedAddress.Board == 0);
        }

        [Theory]
        [InlineData("A0-B0-0")]//Board 0, bankA (0) , Output 0
        public void DecodePdb_COIL_Address2_Tests(string address)
        {
            var decodedAddress = PDBFunctions.DecodePdbAddress(address);
            Assert.True(decodedAddress.Board == 0);
        }
    }
}
