/*
 * Created by: Leslie Sanford
 * 
 * Contact: jabberdabber@hotmail.com
 * 
 * Last modified: 02/17/2004
 */

using System;

namespace Multimedia.Midi
{
	/// <summary>
	/// Represents the basic functionality provided by a device capable of 
	/// receiving Midi messages.
	/// </summary>
	public interface IMidiReceiver
	{
        /// <summary>
        /// Occurs when a channel message is received.
        /// </summary>
        event ChannelMessageEventHandler ChannelMessageReceived;

        /// <summary>
        /// Occures when a system common message is received.
        /// </summary>
        event SysCommonEventHandler SysCommonReceived;

        /// <summary>
        /// Occurs when a system exclusive message is received.
        /// </summary>
        event SysExEventHandler SysExReceived;

        /// <summary>
        /// Occurs when a system realtime message is received.
        /// </summary>
        event SysRealtimeEventHandler SysRealtimeReceived;

        /// <summary>
        /// Occures when an invalid short message is received.
        /// </summary>
        event InvalidShortMessageEventHandler InvalidShortMessageReceived;

        /// <summary>
        /// Starts receiving Midi messages.
        /// </summary>
		void Start();

        /// <summary>
        /// Stops receiving Midi messages.
        /// </summary> 
        void Stop();
	}
}
