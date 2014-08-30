using System.Collections.Generic;

namespace SpearOfLonginus.Animation
{
    /// <summary>
    /// Spear of Longinus' animation class. Supports both delta time and incremental animations with the same codebase.
    /// </summary>
    public class SOLAnimation
    {
        #region Variables

        /// <summary>
        /// The list of frames the animation has.
        /// </summary>
        protected List<SOLFrame> Frames;
        /// <summary>
        /// Whether or not the animation is looping.
        /// </summary>
        protected bool IsLooping;
        /// <summary>
        /// Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.
        /// </summary>
        protected bool ResetIndex;
        /// <summary>
        /// The current frame index of the animation.
        /// </summary>
        protected int CurrentFrame;
        /// <summary>
        /// The animation's timing index.
        /// </summary>
        protected float TimingIndex;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SOLAnimation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        public SOLAnimation(bool loop, bool resetindex)
        {
            Frames = new List<SOLFrame>();
            IsLooping = loop;
            ResetIndex = resetindex; 
            CurrentFrame = 0;
            TimingIndex = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SOLAnimation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        public SOLAnimation(bool loop, bool resetindex, List<SOLFrame> frames)
        {
            Frames = frames;
            IsLooping = loop;
            ResetIndex = resetindex;
            CurrentFrame = 0;
            TimingIndex = 0;
        }

        #endregion

        #region Functions 

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="animspeed">The speed at which to update the animation. You can use this as delta time or an incremental update.</param>
        public virtual void Update(float animspeed)
        {
            TimingIndex += animspeed;

            while (TimingIndex >= GetCurrentFrame().TimeTillNext)
            {
                if (!IsLooping && CurrentFrame == Frames.Count - 1) // If it isn't looping and is on the last frame, ignore the further logic.
                {
                    return;
                }

                if (ResetIndex) //If you're to reset the index...
                {
                    TimingIndex = 0; //Set the timing index to 0. This ensures that each frame will be displayed for at least one frame.
                }
                else
                {
                    TimingIndex -= GetCurrentFrame().TimeTillNext; //Otherwise you'd subtract the time to keep proper timing. This can skip frames if your animation speed gets too fast, however.
                }

                CurrentFrame++;

                if (CurrentFrame == Frames.Count) //Loop as necessary.
                {
                    CurrentFrame = 0;
                }
            }
        }

        /// <summary>
        /// Adds the frame to the list of frames in the animation.
        /// </summary>
        /// <param name="frame">The frame to add.</param>
        public virtual void AddFrame(SOLFrame frame)
        {
            Frames.Add(frame);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public virtual SOLAnimation Clone()
        {
            return new SOLAnimation(IsLooping, ResetIndex, Frames);
        }

        /// <summary>
        /// Gets the number of the frame the animation is on.
        /// </summary>
        /// <returns></returns>
        public virtual int GetFrameNumber()
        {
            return CurrentFrame;
        }

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        /// <returns></returns>
        public virtual SOLFrame GetCurrentFrame()
        {
            return Frames[CurrentFrame];
        }

        /// <summary>
        /// Determines whether or not the animation is finished.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAnimationFinished()
        {
            if (IsLooping) //If it's looping, it'll never be finished.
            {
                return false;
            }

            if (CurrentFrame < Frames.Count - 1) //If it's not on the last frame, it can't possibly be finished.
            {
                return false;
            }

            return TimingIndex >= GetCurrentFrame().TimeTillNext; //If the last frame is done with its timing index, then the animation is finished.
        }

        #endregion
    }
}
