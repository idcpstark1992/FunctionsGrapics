using static UnityEngine.Mathf;
namespace Graphs
{
    public static class Functions
    {
        public static float SineFunction(float _x, float _t)=> Sin(PI* (_x + _t));
        public static float MultiSineFunction(float _x, float _t , float _Period)
        {
            float m_toReturnY = Sin(_Period * (_x + .5f* _t));
            m_toReturnY += .5f* Sin(2f * _Period * (_x + _t));

            return m_toReturnY *(2f/3f);
        }
    }

}
