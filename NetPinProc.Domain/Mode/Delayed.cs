using System;
using NetPinProc.Domain.PinProc;

namespace NetPinProc.Domain.Mode
{
    internal class Delayed : IComparable<Delayed>
    {
        public Delayed(string Name,
            double Time,
            Delegate Handler = null, 
            EventType Event_Type = EventType.SwitchClosedDebounced,
            params object[] Params)
        {
            this.Name = Name;
            this.Time = Time;
            this.Handler = Handler;
            this.Event_Type = Event_Type;
            this.Params = Params;
        }

        public EventType Event_Type { get; set; }
        public Delegate Handler { get; set; }
        public string Name { get; set; }
        public object[] Params { get; set; }
        public double Time { get; set; }
        public int CompareTo(Delayed other) => other.Time.CompareTo(Time);

        public override string ToString() => string.Format("name={0} time={1} event_type={2}", Name, Time, Event_Type);
    }
}
