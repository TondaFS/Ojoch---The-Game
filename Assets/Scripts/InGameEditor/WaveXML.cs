using System.Xml.Serialization;
using System.Collections.Generic;

[XmlInclude(typeof(EditorObjectXML))]
public class WaveXML {
    public string WaveName = "";
    public WaveDifficulty Difficulty = WaveDifficulty.easy;
    public List<EditorObjectXML> Enemies;
    public List<EditorObjectXML> PowerUps;
}
