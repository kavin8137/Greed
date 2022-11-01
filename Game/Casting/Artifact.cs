using System;

namespace Unit4.Game.Casting{
        /// <summary>
        /// <para>An item of cultural or historical interest.</para>
        /// <para>
        /// The responsibility of an Artifact is to provide a value about itself.
        /// </para>
        /// </summary>
    class Artifact : Actor{
        private int _value;
        /// <summary>
        /// None.
        /// </summary>
        public Artifact()
        {

        }

        /// <summary>
        /// Gets the artifact's value.
        /// </summary>
        /// <returns>The vlaue as an integer.</returns>
        public int GetValue()
        {
            return _value;
        }
        
        /// <summary>
        /// Sets the artifact's vlaue to the given value.
        /// </summary>
        /// <param name="value">The given value.</param>
        public void SetValue(int value)
        {
            if (value == 0)
            {
                throw new ArgumentException("fontSize must be greater than zero");
            }
            this._value = value;
        }
    }
}
