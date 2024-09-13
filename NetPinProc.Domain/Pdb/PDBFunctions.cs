using System;
using System.Collections.Generic;
using System.Linq;

namespace NetPinProc.Domain.Pdb
{
    /// <summary>
    /// Helper methods for PDB 
    /// </summary>
    public class PDBFunctions
    {
        /// <summary>max output index on pdled boards</summary>
        const uint PDLED_OUTPUTS = 84;

        /// <summary>
        /// Returns True if the given address is a valid PDB address.
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public static bool IsPdbAddress(string addr, DriverAlias[] aliases = null)
        {
            try
            {
                DecodePdbAddress(addr, aliases);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if given string is a PDBLamp
        /// </summary>
        /// <param name="str"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public static bool IsPDBLamp(string str, DriverAlias[] aliases = null)
        {
            string[] _params = SplitMatrixAddressParts(str);
            if (_params.Length != 2) return false;
            foreach (string addr in _params)
            {
                if (!IsPdbAddress(addr, aliases))
                {
                    Console.WriteLine("Not PDB address! " + addr);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Decodes Ax-By-z or x/y/z into PDB address, Bank number, and Output number. <para/>
        /// Raises exception if it is not a PDB address, otherwise returns a PDBAddress - (addr, Bank, number).
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static PDBAddress DecodePdbAddress(string addr, DriverAlias[] aliases = null)
        {
            if (aliases == null) aliases = new DriverAlias[] { };
            string[] _params;
            PDBAddress address = new PDBAddress();

            foreach (DriverAlias alias in aliases)
            {
                if (alias.Match(addr).Success)
                {
                    addr = alias.Decode(addr);
                    break;
                }
            }

            if (addr.Contains('-'))
            {
                _params = addr.Split('-');

                if (_params.Length != 3)
                    throw new ArgumentOutOfRangeException("PDB address must have 3 components");

                address.Board = (byte)Int32.Parse(_params[0].Substring(1));
                address.Bank = (byte)Int32.Parse(_params[1].Substring(1));
                address.Output = (byte)Int32.Parse(_params[2]);
                return address;
            }
            else if (addr.Contains('/'))
            {
                _params = addr.Split('/');
                Array.Reverse(_params);

                if (_params.Length != 3)
                    throw new ArgumentOutOfRangeException("PDB address must have 3 components");

                address.Board = (byte)Int32.Parse(_params[0]);
                address.Bank = (byte)Int32.Parse(_params[1]);
                address.Output = (byte)Int32.Parse(_params[2]);
                return address;
            }
            else
                throw new ArgumentException("PDB address delimiter (- or /) not found");
        }

        /// <summary>
        /// Input is of form C-Ax-By-z:R-Ax-By-z  or  C-x/y/z:R-x/y/z  or  aliasX:aliasY <para/>
        /// We want to return only the address part: Ax-By-z, x/y/z, or aliasX.  That is, remove the two character prefix if present.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitMatrixAddressParts(string str)
        {
            string[] addrs = str.Split(':');
            Array.Reverse(addrs);
            if (addrs.Length != 2) return new string[] { };
            List<string> addrs_out = new List<string>();

            foreach (string addr in addrs)
            {
                string[] bits = addr.Split('-');
                if (bits.Length == 1)
                    addrs_out.Add(addr);
                else
                {
                    string joinedString = "";
                    for (int i = 1; i < bits.Length; i++)
                    {
                        if (joinedString != "") joinedString += "-" + bits[i];
                        else joinedString = bits[i];
                    }
                    addrs_out.Add(joinedString);
                }
            }
            return addrs_out.ToArray();
        }

        /// <summary>convert string address with board id<para/>
        /// A0-R0-G1-B2</summary>
        /// <param name="address"></param>
        /// <returns>the board id and each id for 3 colors</returns>
        public static List<uint> PdLedRGBAddress(string address)
        {
            var addrs = new List<uint>();

            //take the first number in the array to get the board id
            var crList = address.Split('-');
            addrs.Add(uint.Parse(crList[0].Substring(1)));

            //add the colors            
            foreach (var item in crList.Skip(1))
            {
                addrs.Add(uint.Parse(item.Substring(1)));
            }

            return addrs;
        }

        /// <summary>get addresses from single num<para/></summary>
        /// <param name="ledNumber"></param>
        /// <param name="singleColor"></param>
        /// <returns>the board id and each id for 3 colors</returns>
        public static List<uint> PdLedRGBAddress(uint ledNumber, bool singleColor = false)
        {
            //return colour address with board id at start
            List<uint> addrs = new List<uint>();

            //get the max, for 3 color is 28 or 84
            var maxLedPerBoard = PDLED_OUTPUTS;
            if (!singleColor)
                maxLedPerBoard = maxLedPerBoard / 3;

            //get the start index, eg: 0 = A0-R0 or 28 = A1-R0
            uint startIndex = 0;

            //0-27 limit
            if(ledNumber > (maxLedPerBoard - 1))
            {
                startIndex = ledNumber % maxLedPerBoard;
                if (startIndex > 0)
                    startIndex = startIndex * 3;
            }                
            else if (ledNumber > 0) //add on 3 to the led number
                startIndex = ledNumber * 3;

            //get board id from number
            uint boardId = (uint)Math.Abs(ledNumber / maxLedPerBoard);
            addrs.Add(boardId);

            //add the first address
            addrs.Add(startIndex);

            //add the rest for 3 color
            if(!singleColor)
            {
                addrs.Add(startIndex + 1);
                addrs.Add(startIndex + 2);
            }            

            return addrs;
        }
    }
}
