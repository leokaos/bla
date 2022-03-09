using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer {

    public static Layer PLAYER = new Layer(10);
    public static Layer ENEMY = new Layer(11);

    public int value { get; private set; }

    private Layer(int value) {
        this.value = value;
    }
}
