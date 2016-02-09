/*
 * Created by: Leslie Sanford
 * 
 * Contact: jabberdabber@hotmail.com
 * 
 * Last modified: 02/17/2004
 */

namespace Multimedia.Midi
{
    /// <summary>
    /// Represents the basic functionality provided by a device capable of 
    /// sending Midi messages.
    /// </summary>
    public interface IMidiSender
    {
        /// <summary>
        /// Sends a short Midi message.
        /// </summary>
        /// <param name="message">
        /// The short Midi message to send.
        /// </param>
        /// <param name="status">
        /// Indicates whether or not the status byte should be sent with the 
        /// message.
        /// </param>
        void Send(ShortMessage message, bool status);

        /// <summary>
        /// Sends a system exclusive message.
        /// </summary>
        /// <param name="message">
        /// The system exclusive message to send.
        /// </param>
        void Send(SysExMessage message);

        /// <summary>
        /// Resets MIDI sender.
        /// </summary>
        void Reset();
    }
}
