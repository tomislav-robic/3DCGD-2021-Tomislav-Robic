[System.Serializable]
public class PointsFile
{
    public float points;
    public string name;
    public PointsFile(float _points, string _name)
    {
        points = _points;
        name = _name;
    }
}
