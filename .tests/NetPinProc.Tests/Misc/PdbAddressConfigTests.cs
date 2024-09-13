using NetPinProc.Domain.Pdb;
using Xunit;

namespace NetPinProc.Tests.Misc
{
    public class PdbAddressConfigTests
    {
        [Theory]
        [InlineData("A0-R0-G1-B2", 0)]
        [InlineData("A1-R0-G1-B2", 1)]
        public void DecodePdb_LED_BoardId_AddressTests(string address, int board)
        {
            var addresses = PDBFunctions.PdLedRGBAddress(address);
            Assert.True(addresses[0] == board);
        }

        [Theory]
        [InlineData(27, 0)]
        [InlineData(28, 1)]
        [InlineData(66, 2)]
        public void DecodePdb_LED_BoardId_FromSingleNumber_AddressTests(uint ledNum, int board)
        {
            var addresses = PDBFunctions.PdLedRGBAddress(ledNum);
            Assert.True(addresses[0] == board);
        }

        [Theory]
        [InlineData(0, 0, 1, 2)]
        [InlineData(1, 3, 4, 5)]
        [InlineData(27, 81, 82, 83)]
        [InlineData(28, 0, 1, 2)]
        [InlineData(29, 3, 4, 5)]
        [InlineData(55, 81, 82, 83)]
        [InlineData(56, 0, 1, 2)]
        public void DecodePdb_LED_Indexes_FromSingleNumber_AddressTests(uint ledNum, uint a1, uint a2, uint a3)
        {
            var addresses = PDBFunctions.PdLedRGBAddress(ledNum);
            Assert.True(addresses[1] == a1);
            Assert.True(addresses[2] == a2);
            Assert.True(addresses[3] == a3);
        }

        [Theory]
        [InlineData("0/0/0")]//Board 0, bankA (0) , Output 0
        public void DecodePdb_COIL_AddressTests(string address)
        {
            var decodedAddress = PDBFunctions.DecodePdbAddress(address);
            Assert.True(decodedAddress.Board == 0);
        }

        /// <summary>TODO: this address returns number 40 in game</summary>
        /// <param name="address"></param>
        [Theory]
        [InlineData("A0-B0-0")]//Board 0, bankA (0) , Output 0
        public void DecodePdb_COIL_Address2_Tests(string address)
        {
            var decodedAddress = PDBFunctions.DecodePdbAddress(address);
            Assert.True(decodedAddress.Board == 0);
        }
    }
}
