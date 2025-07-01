using System;

namespace Data
{
    [Serializable]
    public class ItemData
    {
        public Vector3Modify Position { get; set; }
        public float Visibility { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }
}