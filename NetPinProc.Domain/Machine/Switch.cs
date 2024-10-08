﻿using NetPinProc.Domain.PinProc;
using System;

namespace NetPinProc.Domain
{
    /// <summary>
    /// Represents a switch in a pinball machine. <para/>
    /// Switches are accessed using <see cref="IGameController.Switches"/>
    /// </summary>
    public class Switch : GameItem
    {
        /// <summary>
        /// The current state of the switch.
        /// 'True' for closed, 'False' for open.
        /// 
        /// Generally, use IsActive() or IsInactive() instead of this
        /// </summary>
        protected bool _state = false;

        /// <summary>
        /// The last time of the change of this switch.
        /// </summary>
        protected DateTime _last_changed = DateTime.Now;

        /// <summary>
        /// The type of switch this is. NO for Normally Open, NC for Normally Closed
        /// </summary>
        protected SwitchType _type = SwitchType.NO;

        /// <summary>
        /// Creates a new Switch object with <see cref="SwitchType"/>
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="name"></param>
        /// <param name="number">The encoded number of this switch</param>
        /// <param name="sType">Switch type NO = Normally Open (leaf switches), NC = Normally Closed (optos)</param>
        public Switch(IProcDevice proc, string name, ushort number, SwitchType sType = SwitchType.NO)
            : base(proc, name, number) => this._type = sType;

        /// <summary>
        /// Sets the switch state to the given state
        /// </summary>
        /// <param name="state">true if active, false otherwise</param>
        public void SetState(bool state)
        {
            this._state = state;
            ResetTimer();
        }

        /// <summary>
        /// Check if the switch is the given state (or has been in the given state for the specified number of seconds)
        /// </summary>
        /// <param name="state">The state to check</param>
        /// <param name="seconds">The time the switch has been in the specified state (secs)</param>
        /// <returns>True if the switch has been in the specified state for the given number of seconds (or is currently in the state if seconds is unspecified)</returns>
        public bool IsState(bool state, double seconds = -1)
        {
            if (this._state == state)
                if (seconds != -1)
                    return this.TimeSinceChange() > seconds;
                else
                    return true;
            else
                return false;
        }

        /// <summary>
        /// Checks to see if the switch is currently active or has been active for the specified number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds the switch has been active</param>
        /// <returns>True if the switch is active or has been for the specified number of seconds, false otherwise</returns>
        public bool IsActive(double seconds = -1)
        {
            if (this._type == SwitchType.NO)
                return this.IsState(true, seconds);
            else
                return this.IsState(false, seconds);
        }

        /// <summary>
        /// Checks to see if the switch is currently inactive or has been inactive for the specified number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds the switch has been inactive</param>
        /// <returns>True if the switch is inactive or has been for the specified number of seconds, false otherwise</returns>
        public bool IsInactive(double seconds = -1)
        {
            if (this._type == SwitchType.NC)
                return this.IsState(true, seconds);
            else
                return this.IsState(false, seconds);
        }

        /// <summary>
        /// Checks to see if the switch is currently open or has been for the specified number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds this switch has been open for</param>
        /// <returns>True if the switch is currently open or has been open for the specified number of seconds, false otherwise.</returns>
        public bool IsOpen(double seconds = -1) => this.IsState(false, seconds);

        /// <summary>
        /// Checks to see if the switch is currently closed or has been for the specified number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds this switch has been closed for</param>
        /// <returns>True if the switch is currently closed or has been closed for the specified number of seconds, false otherwise.</returns>
        public bool IsClosed(double seconds = -1) => this.IsState(true, seconds);

        /// <summary>
        /// Get the number of seconds since the switch has last changed states
        /// </summary>
        /// <returns>The number of seconds since the switch has last changed states</returns>
        public double TimeSinceChange() => DateTime.Now.Subtract(_last_changed).TotalSeconds;

        /// <summary>
        /// Set the time of last state change to the current time
        /// </summary>
        public void ResetTimer() => this._last_changed = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>closed or open</returns>
        public string StateString()
        {
            if (this.IsClosed())
                return "closed";
            else
                return "open";
        }

        /// <summary>Gets the switch type</summary>
        public SwitchType Type => _type;
        
        /// <returns>Name and number</returns>
        public override string ToString() => $"<Switch name={Name} number={Number}/>";
    }
}
