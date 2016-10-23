﻿using System.Drawing;
using CustomInput;

namespace System.Windows.Forms.GamePlayer
{
    /// <summary>
    /// All required functions for a Windows Form Game Player to run one portion of your game
    /// </summary>
    public abstract class Scene
    {
        /// <summary>
        /// Keyboard controls
        /// </summary>
        protected Keyboard KeyboardControl;

        /// <summary>
        /// Available mouse controls
        /// </summary>
        protected Mouse MouseControl;

        /// <summary>
        /// Information about the game environment
        /// </summary>
        protected GameData GameData;

        /// <summary>
        /// The scene that should be set after the current tick/draw cycle
        /// </summary>
        public Scene nextScene { get; protected set; }

        /// <summary>
        /// Called when the scene is first made
        /// </summary>
        /// <param name="controller"></param>
        protected Scene(GamePlayerControl controller)
        {
            GameData.PictureBoxHeight = controller.Height;
            GameData.PictureBoxWidth = controller.Width;
            GameData.TicksPerSecond = controller.TicksPerSecond;
            GameData.ExpectedDt = 1.0/controller.TicksPerSecond;

            KeyboardControl = new Keyboard(controller);
            MouseControl = new Mouse(controller);
        }

        /// <summary>
        /// Draws the current playable
        /// </summary>
        /// <param name="g">The graphics object for the PictureBox</param>
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Called whenever the timer ticks. Calls <see cref="KeyboardControl.Tick()"/> and <see cref="MouseControl.Tick()"/>
        /// </summary>
        public virtual void Tick()
        {
            KeyboardControl.Tick();
            MouseControl.Tick();
            GameData.TickCount++;
        }

        internal void resetNextScene()
        {
            nextScene = null;
        }
        internal void setActualDt(double dt)
        {
            GameData.actualDt = dt;
        }
    }
}