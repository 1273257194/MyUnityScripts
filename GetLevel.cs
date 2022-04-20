using system;
public class GetLevel 
{
    public string levelInfo;
    public int score;
 
    public void GetLevelInfo()
    {
        levelInfo = score < 40 ? "E" : score < 50 ? "D" : score <60 ? "C" : score< 70 ? "B" : score < 80 ? "A" : score < 90 ? "S" : "SS";
    }

}