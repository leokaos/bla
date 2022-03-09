using UnityEngine;

public class SortingLayer {

    public static SortingLayer BACKGROUND = new SortingLayer("Background");
    public static SortingLayer FOREGROUND = new SortingLayer("Foreground");
    public static SortingLayer PSortingLayer = new SortingLayer("PSortingLayer");
    public static SortingLayer GROUND = new SortingLayer("Ground");

    private string name;

    private SortingLayer(string name) {
        this.name = name;
    }

    public bool IsSame(string name) {
        return string.Equals(this.name, name, System.StringComparison.OrdinalIgnoreCase);
    }
}
