using NetPinProc.Domain.PinProc;
using System.Collections.Generic;

namespace NetPinProc.Domain
{
    /// <summary>
    /// A mode queue for <see cref="IMode"/>s
    /// </summary>
    public interface IModeQueue
    {
        /// <summary>Mode list</summary>
        List<IMode> Modes { get; }

        /// <summary>Add a mode to the list</summary>
        /// <param name="mode"></param>
        void Add(IMode mode);

        /// <summary>Clear all modes from queue</summary>
        void Clear();

        /// <summary>Processes every mode in queue for accepted_switches to invoke or delay</summary>
        /// <param name="evt"></param>
        void HandleEvent(Event evt);

        /// <summary>Remove a mode from queue</summary>
        /// <param name="mode"></param>
        void Remove(IMode mode);

        /// <summary>On Tick from game controller</summary>
        void Tick();

        /// <summary>Get a print list of running modes</summary>
        /// <returns></returns>
        string ToString();
    }
}