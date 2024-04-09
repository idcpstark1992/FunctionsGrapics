using UnityEngine;

namespace Graphs
{
    public  class GraphSine : MonoBehaviour
    {
        [SerializeField] private Transform              PointPrefabTransform;
        [SerializeField,Range(10,80)] private int       CubesAmount;
        [SerializeField, Range(2, 6)] private int       Period;
        private float _time = 0f;
        protected Transform[] PointsTrackedPositions;
        private void Start()=> CreateGraph();
        private void CreateGraph()
        {
            PointsTrackedPositions = new Transform[CubesAmount];

            // this must include the "F" character. otherwise, we are going to have a problem.
            float   m_steps = 2f / CubesAmount;
            Vector3 m_position  = Vector3.zero;
            Vector3 m_scale     = Vector3.one * m_steps;

            GameObject m_Parent             = new();
            m_Parent.transform.name         ="Parent Object";
            m_Parent.transform.position     = Vector3.zero;
            
            for (int i = 0; i < PointsTrackedPositions.Length; i++)
            {
                float m_xPosition = (i + 0.5f) * m_steps - 1f;
                float m_yPosition = m_xPosition * m_xPosition ;

                m_position.Set(m_xPosition, m_yPosition, 0);
                
                Transform m_toInstantiatePoint  = PointsTrackedPositions[i]  =Instantiate(PointPrefabTransform);

                m_toInstantiatePoint.localScale = m_scale;
                m_toInstantiatePoint.position = m_position;
                m_toInstantiatePoint.SetParent(m_Parent.transform);
            }
        }
        private void Update() 
        {
            _time = Time.time;
            for (int i = 0; i < PointsTrackedPositions.Length; i++)
            {
                Transform m_trasform = PointsTrackedPositions[i];
                Vector3 m_currentPosition = m_trasform.localPosition;
                m_currentPosition.y = Functions.MultiSineFunction(m_currentPosition.x, _time, Mathf.PI);
                m_trasform.localPosition = m_currentPosition;
            }
        }
    }
}
