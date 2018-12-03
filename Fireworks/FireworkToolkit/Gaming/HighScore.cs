using FireworkToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FireworkToolkit.Gaming
{
    public class HighScore : IComparable<HighScore>, IFilable
    {
        #region Properties

        public string Name { get; set; } = "";

        public int Score { get; set; } = 0;

        #endregion

        public HighScore(string name = "", int score = 0)
        {
            Name = name;
            Score = score;
        }

        public int CompareTo(HighScore other)
        {
            return Score.CompareTo(other.Score);
        }

        public XElement GetElement()
        {
            return new XElement("Highscore", new XAttribute("Name", Name), new XAttribute("Score", Score));
        }

        public void FromElement(XElement e)
        {
            if (e.Name == "Highscore")
            {
                Name = (string)e.Attribute("Name");
                Score = (int)e.Attribute("Score");
            }
        }
    }
}
