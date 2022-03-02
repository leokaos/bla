
public class Tags {

    public static string PLAYER = "Player";
    public static bool isTag(string value, string tag) {
        return string.Equals(value, tag, System.StringComparison.OrdinalIgnoreCase);
    }

}
