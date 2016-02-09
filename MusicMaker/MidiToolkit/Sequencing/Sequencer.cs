/*
 * Created by: Leslie Sanford
 * 
 * Contact: jabberdabber@hotmail.com
 * 
 * Last modified: 05/08/2004
 */

using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace Multimedia.Midi
{
	/// <summary>
	/// Summary description for Sequencer
	/// </summary>
	public class Sequencer : Component, IMidiMessageVisitor
	{
        #region Sequencer Members

        #region Fields

        // The sequence to play.
        private Sequence sequence;

        // Tick generator for tick events.
        private TickGenerator tickGen = new TickGenerator();

        // Midi sender for sending Midi messages.
        private IMidiSender midiSender;

        // Playback position.
        private int position = 0;

        // Current Midi event index into the sequence.
        private int currIndex = 0;

        // Current tick count.
        private int currTicks = 0;

        // Running status value.
        private int runningStatus = 0;

        // Merged track from the sequence for playback.
        private Track mergedTrack = new Track();  
      
        #endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        #region Construction

        /// <summary>
        /// Initialize a new instance of the Sequencer class with the specified
        /// Container.
        /// </summary>
        /// <param name="container">
        /// Container for holding components.
        /// </param>
		public Sequencer(System.ComponentModel.IContainer container)
		{
			//
			// Required for Windows.Forms Class Composition Designer support
			//
			container.Add(this);
			InitializeComponent();

            // Register tick handler.
			tickGen.Tick += new TickEventHandler(tickGen_Tick);
		}

        /// <summary>
        /// Initializes a new instance of the Sequencer class.
        /// </summary>
		public Sequencer()
		{
			//
			// Required for Windows.Forms Class Composition Designer support
			//
			InitializeComponent();

            // Register tick handler.
			tickGen.Tick += new TickEventHandler(tickGen_Tick);
		}

        #endregion

        #region Methods

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}

                tickGen.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion        

        /// <summary>
        /// Attaches a handler to the internal tick generator. This allows 
        /// outside sources to sync with the sequencer's playback.
        /// </summary>
        /// <param name="handler">
        /// The delegate to attach to the tick generator.
        /// </param>
        public void AttachToClock(TickEventHandler handler)
        {
            tickGen.Tick += handler;
        }

        /// <summary>
        /// Detaches a handler from the internal tick generator. 
        /// </summary>
        /// <param name="handler">
        /// The delegate to detach from the tick generator.
        /// </param>
        public void DetachFromClock(TickEventHandler handler)
        {
            tickGen.Tick -= handler;
        }                                  

        /// <summary>
        /// Starts playback.
        /// </summary>
        public void Start()
        {
            // If there is a sequence and MidiSender present.
            if(Sequence != null && MidiSender != null)
            {
                // If the sequencer is already playing.
                if(IsPlaying())
                {
                    // Stop playback.
                    Stop();
                }

                // Reset position to the beginning of the sequence.
                Position = 0;

                // Start the tick generator.
                tickGen.Start();
            }
        }

        /// <summary>
        /// Stops playback.
        /// </summary>
        public void Stop()
        {
            // If the sequencer is currently playing.
            if(IsPlaying())
            {
                // Stop the tick generator.
                tickGen.Stop();

                // Reset the MidiSender - causes all currently playing Note to
                // be turned off.
                midiSender.Reset();

                // Reset running status.
                runningStatus = 0;
            }
        }

        /// <summary>
        /// Continues playback.
        /// </summary>
        public void Continue()
        {
            // If there is a sequence and MidiSender present.
            if(Sequence != null && MidiSender != null)
            {
                // If the sequencer is not playing.
                if(!IsPlaying())
                {
                    // Start playback. Playback will continue from the current
                    // position.
                    tickGen.Start();
                }
            }
        }

        /// <summary>
        /// Determines whether or not the sequencer is playing.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the sequencer is playing; otherwise, <b>false</b>.
        /// </returns>
        public bool IsPlaying()
        {
            return tickGen.IsRunning();
        }

        /// <summary>
        /// Updates the sequencer position.
        /// </summary>
        private void UpdatePosition()
        {
            Position = position;
        }

        /// <summary>
        /// Handles tick events generated by the tick generator.
        /// </summary>
        /// <param name="sender">
        /// The tick generator responsible for the event.
        /// </param>
        /// <param name="e">
        /// Holds information about the Tick event.
        /// </param>
        private void tickGen_Tick(object sender, TickEventArgs e)
        {
            // Keep track of position.
            position += e.Ticks;

            // Keep track of the current tick tounc.
            currTicks += e.Ticks;

            // While the end of the track has not been reached and the 
            // current tick count is greater than or equal to the tick count 
            // for the next Midi event.
            while(currIndex < mergedTrack.Count && 
                currTicks >= mergedTrack[currIndex].Ticks)
            {
                // Visit the message.
                mergedTrack[currIndex].Message.Accept(this);

                // Remove the ticks from the Midi event from the current tick
                // count.
                currTicks -= mergedTrack[currIndex].Ticks;

                // Move to next message in the merged track.
                currIndex++;
            }  
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Midi sender to use for sending messages.
        /// </summary>
        public IMidiSender MidiSender
        {
            get
            {
                return midiSender;
            }
            set
            {
                midiSender = value;
            }
        }

        /// <summary>
        /// Gets or sets the playback position.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if position is set less than zero.
        /// </exception>
        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                // Enforce preconditions.
                if(value < 0)
                    throw new ArgumentOutOfRangeException("Position", value,
                        "Negative values for sequencer position not allowed.");

                // Initialize position.
                position = value;

                // If the sequencer has a sequence.
                if(Sequence != null)
                { 
                    // Tick counter.
                    int p = 0;                        

                    // If the sequencer is playing.
                    if(IsPlaying())
                    {
                        // Stop playback.
                        Stop();
                    }

                    currIndex = currTicks = 0;

                    // Get merged track from sequence.
                    mergedTrack = sequence.GetMergedTrack();
                        
                    // Find the Midi event at the new position in merged track.
                    while(currIndex < mergedTrack.Count && p < position)
                    {
                        // Accumulate ticks from Midi events.
                        p += mergedTrack[currIndex].Ticks;

                        // If the new position has not been reached yet.
                        if(p < position)
                        {
                            // Move to next event.
                            currIndex++;
                        }
                    }

                    // If the end of the merged track has not been reached.
                    if(currIndex < mergedTrack.Count)
                    {
                        //
                        // Calculate the current tick count based on the 
                        // new position.
                        //

                        p -= position;
                        currTicks = mergedTrack[currIndex].Ticks - p;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the sequence for the sequencer.
        /// </summary>
        public Sequence Sequence
        {
            get
            {
                return sequence;
            }
            set
            {
                // Enforce preconditions.
                if(value != null && value.IsSmpte())
                    throw new ArgumentException(
                        "Smpte sequences are not yet supported");

                // If the sequencer is playing.
                if(IsPlaying())
                {
                    // Stop playback.
                    Stop();
                }

                // Initialize new sequence.
                sequence = value;

                if(sequence != null)
                {
                    // Initialize tick generator.
                    tickGen.TicksPerBeat = sequence.Division;
                }

                UpdatePosition();                
            }
        }

        /// <summary>
        /// Gets or sets the tempo in microseconds.
        /// </summary>
        public int Tempo
        {
            get
            {
                return tickGen.Tempo;
            }
            set
            {
                tickGen.Tempo = value;
            }
        }

        #endregion

        #endregion

        #region IMidiMessageVisitor Members

        /// <summary>
        /// Visits channel messages.
        /// </summary>
        /// <param name="message">
        /// The ChannelMessage to visit.
        /// </param>
        public void Visit(ChannelMessage message)
        {
            // If the running status does not match the channel message's 
            // status.
            if(runningStatus != message.Status)
            {
                // Update status value.
                runningStatus = message.Status;

                // Send message with status value.
                MidiSender.Send(message, true);
            }
            // Else the running status matches the channel message's status.
            else
            {
                // Send message without status value.
                MidiSender.Send(message, false);
            } 
        }

        /// <summary>
        /// Visits meta messages.
        /// </summary>
        /// <param name="message">
        /// The MetaMessage to visit.
        /// </param>
        void Multimedia.Midi.IMidiMessageVisitor.Visit(MetaMessage message)
        {
            // If the meta message is a tempo change message type.
            if(message.Type == MetaType.Tempo)
            {
                // Change tempo.
                Tempo = MetaMessage.PackTempo(message);
            }
            // Else if the meta message is an end of track message type.
            else if(message.Type == MetaType.EndOfTrack)
            {
                // Stop playback.
                Stop();
            }
        }

        /// <summary>
        /// Visits system common messages.
        /// </summary>
        /// <param name="message">
        /// The SysCommonMessage to visit.
        /// </param>
        void Multimedia.Midi.IMidiMessageVisitor.Visit(SysCommonMessage message)
        {
            // Nothing to do here.
        }

        /// <summary>
        /// Visits system exclusive messages.
        /// </summary>
        /// <param name="message">
        /// The SysExMessage to visit.
        /// </param>
        void Multimedia.Midi.IMidiMessageVisitor.Visit(SysExMessage message)
        {
            // System exclusive messages cancel running status.
            runningStatus = 0;

            // Send system exclusive message.
            MidiSender.Send(message);                
        }

        /// <summary>
        /// Visits system realtime messages. 
        /// </summary>
        /// <param name="message">
        /// The system realtime message to visit.
        /// </param>
        void Multimedia.Midi.IMidiMessageVisitor.Visit(SysRealtimeMessage message)
        {
            // Nothing to do here.
        }

        #endregion
	}
}
