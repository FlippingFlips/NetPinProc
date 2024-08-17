using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Sqlite.Tests
{
    public class AdjustmentMenuTests
    {
        [Fact]
        public void ConvertRangeOptionType()
        {
            using var ctx = new NetProcDbContext();
            ctx.InitializeDatabase(true, true);

            var adjustments = ctx.Adjustments.ToList();
            var adj = adjustments.GroupBy(x => x.SubMenuName);
            foreach ( var adjItem in adj )
            {
                foreach (var item in adjItem)
                {
                    var rangeOptions = item.Options.Split(",");

                    switch (item.OptionType)
                    {
                        case OptionType.Range:
                            
                            if(rangeOptions.Length == 2)
                            {
                                int.TryParse(rangeOptions[0], out int low);
                                int.TryParse(rangeOptions[1], out int hi);
                            }
                            break;
                        case OptionType.Array:
                            List<int> _vals = new();
                            foreach ( var val in rangeOptions)
                            {
                                int.TryParse(val, out int opt);
                                _vals.Add(opt);
                            }
                            break;
                        case OptionType.Enum: //split array by index is enough for enum
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
