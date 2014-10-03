using System.Collections.Generic;

namespace SpearOfLonginus.Animations
{
    /// <summary>
    /// Spear of Longinus' animation class. Supports both delta time and incremental animations with the same codebase.
    /// </summary>
    public class Animation
    {
        #region Variables

        /// <summary>
        /// The list of frames the animation has.
        /// </summary>
        protected List<Frame> Frames;

        /// <summary>
        /// Whether or not the animation is looping.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is looping]; otherwise, <c>false</c>.
        /// </value>
        public bool IsLooping { get; protected set; }

        /// <summary>
        /// Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [reset index]; otherwise, <c>false</c>.
        /// </value>
        public bool ResetIndex { get; protected set; }

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
        /// Initializes a new instance of the <see cref="Animation" /> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        public Animation(bool loop, bool resetindex)
        {
            Frames = new List<Frame>();
            IsLooping = loop;
            ResetIndex = resetindex; 
            CurrentFrame = 0;
            TimingIndex = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation" /> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        public Animation(bool loop, bool resetindex, List<Frame> frames)
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
        /// <param name="deltatime">The speed at which to update the animation. You can use this as delta time or an incremental update.</param>
        public virtual void Update(float deltatime)
        {
            TimingIndex += deltatime;

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
        public virtual void AddFrame(Frame frame)
        {
            Frames.Add(frame);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public virtual Animation Clone()
        {
            return new Animation(IsLooping, ResetIndex, GetFramesList());
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
        public virtual Frame GetCurrentFrame()
        {
            return Frames[CurrentFrame];
        }

        /// <summary>
        /// Gets a copy of the list of frames.
        /// </summary>
        /// <returns></returns>
        public virtual List<Frame> GetFramesList()
        {
            return new List<Frame>(Frames.ToArray());
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
