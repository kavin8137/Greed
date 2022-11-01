using System.Collections.Generic;
using Unit4.Game.Casting;
using Unit4.Game.Services;
using System;


namespace Unit4.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService _keyboardService = null;
        private VideoService _videoService = null;
        private int _score;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this._keyboardService = keyboardService;
            this._videoService = videoService;
            this._score = 0;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            _videoService.OpenWindow();
            while (_videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            _videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = _keyboardService.GetDirection();
            robot.SetVelocity(velocity); 
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> artifacts = cast.GetActors("artifacts");

            banner.SetText($"Score: {_score}");
            int maxX = _videoService.GetWidth();
            int maxY = _videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            foreach (Actor actor in artifacts)
            {
                Random number = new Random();
                Point aVelocity = new Point(0, 1);
                aVelocity = aVelocity.Scale(10);
                actor.SetVelocity(aVelocity);
                actor.MoveNext(maxX,maxY);

                Artifact artifact = (Artifact) actor;
                if (artifact.GetText() == "O")
                {
                    artifact.SetValue(-1);
                }
                else if (artifact.GetText() == "*")
                {
                    artifact.SetValue(1);
                }


                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    _score += artifact.GetValue();
                    banner.SetText($"Score: {_score}");
                    int xPos = number.Next(1,100);
                    Point newPos = new Point(xPos,0);
                    newPos = newPos.Scale(30);
                    artifact.SetPosition(newPos);
                }
            } 
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            _videoService.ClearBuffer();
            _videoService.DrawActors(actors);
            _videoService.FlushBuffer();
        }

    }
}