using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerializableTypes
{
    [Serializable]
    public class SerializeVector3
    {
        float x, y, z;

        public SerializeVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public SerializeVector3(Vector3 newValue)
        {
            this.x = newValue.x;
            this.y = newValue.y;
            this.z = newValue.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }

    [Serializable]
    public class SerializeQuaternion
    {
        float x, y, z, w;

        public SerializeQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public SerializeQuaternion(Quaternion newValue)
        {
            this.x = newValue.x;
            this.y = newValue.y;
            this.z = newValue.z;
            this.w = newValue.w;
        }

        public Quaternion ToQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }
    }

    [Serializable]
    public class SerializeColor
    {
        float r = 0, g = 0, b = 0, a = 0;

        public SerializeColor(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Color getColor()
        {
            return new Color(r, g, b, a);
        }

        public static SerializeColor fromColor(Color newColor)
        {
            return new SerializeColor(newColor.r, newColor.g, newColor.b, newColor.a);
        }
    }

    [Serializable]
    public class SerialzeTransform
    {
        [SerializeField]
        public SerializeVector3 position;
        [SerializeField]
        public SerializeVector3 scale;
        [SerializeField]
        public SerializeQuaternion rotation;

        public SerialzeTransform(Transform objTransform)
        {
            position = new SerializeVector3(objTransform.position);
            scale = new SerializeVector3(objTransform.localScale);
            rotation = new SerializeQuaternion(objTransform.rotation);
        }
    }
};
