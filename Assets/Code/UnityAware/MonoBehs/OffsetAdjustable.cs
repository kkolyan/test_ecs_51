using UnityEngine;

namespace UnityAware.MonoBehs
{
    public class OffsetAdjustable: MonoBehaviour, IAdjustable
    {
        public Transform mobilePart;
        public Vector3 offsetWhenOpened;
        
        public void SetValue(float value)
        {
            mobilePart.transform.localPosition = offsetWhenOpened * value;
        }
    }
}