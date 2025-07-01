using System;

namespace Data
{
    [Serializable]
    public class Vector3Modify
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3Modify(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}