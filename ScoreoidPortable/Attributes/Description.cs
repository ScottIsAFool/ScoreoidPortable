using System;

namespace ScoreoidPortable
{
    internal class Description : Attribute
    {
        public string Text;

        public Description(string text)
        {
            Text = text;
        }
    }
}