using UnityEngine;

[System.Serializable]

public struct SerializableVector3 {
    public float x;
    public float y;
    public float z;

    public SerializableVector3(float X, float Y, float Z) {
        this.x = X;
        this.y = Y;
        this.z = Z;
    }

    // convert from this struct to a Unity Vector3
    public Vector3 ToVector3() {
        return new Vector3(x, y, z);
    }

    // convert from Unity Vector3 to this struct type of serializeable vector
    public static SerializableVector3 ToSerializeable(Vector3 vector) {
        return new SerializableVector3(vector.x, vector.y, vector.z);
    }
}

