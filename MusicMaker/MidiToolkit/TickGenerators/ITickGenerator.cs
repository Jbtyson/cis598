/*
 * Created by: Leslie Sanford
 * 
 * Contact: jabberdabber@hotmail.com
 * 
 * Last modified: 04/08/2004
 */

using System;

namespace Multimedia.Midi
{
	/// <summary>
	/// Represents the base functionality of a tick generator.
	/// </summary>
	public interface ITickGenerator
	{
        /// <summary>
        /// Occurs when ticks are generated.
        /// </summary>
		event TickEventHandler Tick;

        /// <summary>
        /// Starts the tick generator.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the tick generator.
        /// </summary>
        void Stop();

        /// <summary>
        /// Determines whether or not the tick generator is running.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the tick generator is running; otherwise, 
        /// <b>false</b>.
        /// </returns>
        bool IsRunning();

	}
}
