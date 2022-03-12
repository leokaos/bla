using UnityEngine;

public class Tag {

    public static Tag GROUND = new Tag("Ground");
    public static Tag PLAYER = new Tag("Player");
    public static Tag ENEMY = new Tag("Enemy");

    private string name;

    private Tag(string name) {
        this.name = name;
    }

    public bool IsSame(string tagName) {
        return string.Equals(this.name, tagName, System.StringComparison.OrdinalIgnoreCase);
    }

    public bool IsSame(Collider2D collision) {
        return string.Equals(this.name, collision.tag, System.StringComparison.OrdinalIgnoreCase);
    }

}
