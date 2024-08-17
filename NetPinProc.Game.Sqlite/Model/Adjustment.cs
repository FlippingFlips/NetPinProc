#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace NetPinProc.Game.Sqlite.Model
{
    public class Adjustment
    {
        /// <summary>
        /// Should be used like BALLS_PER_GAME. Then keys like this can be used for transaltion.
        /// </summary>
        public string Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Name of menu belongs to. eg: Standard Adjustments
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// Sub menu to group items
        /// </summary>
        public string SubMenuName { get; set; }
        public string Description { get; set; }
        public int ValueDefault { get; set; }
        public int Value { get; set; }
        public OptionType OptionType { get; set; }
        public string Options { get; set; }
    }

    public enum OptionType
    {
        Range,
        Array,
        Enum
    }
}
