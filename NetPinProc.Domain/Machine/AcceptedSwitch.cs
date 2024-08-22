using System;
using NetPinProc.Domain.PinProc;

namespace NetPinProc.Domain.Machine
{
    /// <inheritdoc/>
    internal class AcceptedSwitch : IEquatable<AcceptedSwitch>
    {
        public AcceptedSwitch(string Name, EventType Event_Type, double Delay, SwitchAcceptedHandler Handler = null, object Param = null)
        {
            this.Name = Name;
            this.Event_Type = Event_Type;
            this.Delay = Delay;
            this.Handler = Handler;
            this.Param = Param;
        }

        public double Delay { get; set; }
        public EventType Event_Type { get; set; }
        public SwitchAcceptedHandler Handler { get; set; }
        public string Name { get; set; }
        public object Param { get; set; }
        public bool Equals(AcceptedSwitch other)
        {
            if (other.Delay == Delay && other.Name == Name
                && other.Event_Type == Event_Type && other.Handler == Handler)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format("<name={0} event_type={1} delay={2}>", Name, Event_Type, Delay);
        }
    }
}
