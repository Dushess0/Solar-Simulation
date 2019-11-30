using UnityEngine;

namespace MLAgents
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        Vector3 m_Offset;

        // Use this for initialization
        void Start()
        {
            m_Offset = gameObject.transform.position - target.position;
        }

        // Update is called once per frame
        void Update()
        {

            Debug.Log(target.transform.position);
            transform.position = target.position + m_Offset;
        }
    }
}
