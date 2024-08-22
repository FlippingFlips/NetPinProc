using System.Text.RegularExpressions;

namespace NetPinProc.Domain.Pdb
{
    public class DriverAlias
    {
        public Regex expr;
        string repl;
        public DriverAlias(string key, string value)
        {
            this.expr = new Regex(key);
            this.repl = value;
        }

        public MatchCollection Matches(string addr) => expr.Matches(addr);

        public Match Match(string addr) => expr.Match(addr);

        public string Decode(string addr) => expr.Replace(addr, repl);
    }
}
