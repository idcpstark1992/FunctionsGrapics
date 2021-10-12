using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGridScript : MonoBehaviour
{
    [SerializeField] private int GraphicXLenght;
    [SerializeField] private int GraphicYLenght;
    [SerializeField] private bool UseNegativeValues;

    [SerializeField] private LineRenderer[] YGridLinesArray;
    [SerializeField] private LineRenderer[] XGridLinesArray;

    [SerializeField] private LineRenderer GraphLineRender;
    [SerializeField] private LineRenderer YAxisLine;
    [SerializeField] private LineRenderer XAxisLine;
    [SerializeField] private float NumerationSteps;

    [SerializeField] private List<Vector3> FunctionPoints;


    [SerializeField] private int InteractionsAmount;
    [Header("From where to where do you want to Sample Graphics")]
    [SerializeField] private float MinimunSampleValue;
    [SerializeField] private float MaximunSampleValue;
    [SerializeField] private float GraphicResolution;

    [SerializeField] private PrintNumber PrintNumberPrefab;


    void Start()
    {
        GenerateLineAxisLines();
    }

    private void GenerateLineAxisLines()
    {
        Vector3 MinimunXPoint = new Vector3(UseNegativeValues ? GraphicXLenght * -1 : 0f, 0f, 0f);
        Vector3 MaximunXPoint = Vector3.right * GraphicXLenght;
        Vector3[] InnerXPositions = new Vector3[2] { MinimunXPoint, MaximunXPoint };
        XAxisLine.SetPositions(InnerXPositions);
        Vector3 MinimunYPoint = new Vector3(0f, UseNegativeValues ? GraphicYLenght * -1 : 0f, 0f);
        Vector3 MaximunYPoint = Vector3.up * GraphicYLenght;
        Vector3[] InnerYPositions = new Vector3[2] { MinimunYPoint, MaximunYPoint };
        YAxisLine.SetPositions(InnerYPositions);

        GenerateAxisNumbers(MinimunXPoint.x, MaximunXPoint.x, MinimunYPoint.y, MaximunYPoint.y);

        GenerateGraphics();
    }

    private void GenerateAxisNumbers(float _minimunXpoint , float _maximunXpoint , float _minimunYPoint , float _maximunYPoint)
    {
        GameObject NumerationHolder = new GameObject("NumerationParent");
        Vector3 NumberInGrid = new Vector3();

        NumberInGrid.y = -.2f;

        for (float x = _minimunXpoint; x < _maximunXpoint; x+= NumerationSteps)
        {
            NumberInGrid.x = x;
            PrintNumber mToInstantiate= Instantiate(PrintNumberPrefab, NumerationHolder.transform);
            mToInstantiate.InitNumber(x);
            mToInstantiate.transform.position = NumberInGrid;
        }

        NumberInGrid.x = .2f;
        for (float y = _minimunXpoint; y < _maximunXpoint; y += NumerationSteps)
        {
            NumberInGrid.y = y;
            PrintNumber mToInstantiate = Instantiate(PrintNumberPrefab, NumerationHolder.transform);
            mToInstantiate.InitNumber(y);
            mToInstantiate.transform.position = NumberInGrid;
        }
    }
    public void GenerateGraphics()
    {
        Vector3 newGraphPosition = Vector3.zero;
        for (float i = MinimunSampleValue; i < MaximunSampleValue; i+= GraphicResolution)
        {
            newGraphPosition.x = i;
            newGraphPosition.y = FunctionsLibrary.GetYValueLinear(i);
            FunctionPoints.Add(newGraphPosition);
        }
        GraphLineRender.positionCount = FunctionPoints.Count;
        GraphLineRender.SetPositions(FunctionPoints.ToArray());
    }

}
public static class FunctionsLibrary
{
    /// <summary>
    /// funcion afin
    /// </summary>
    /// <param name="_Xvalue"></param>
    /// <returns></returns>
    public static float GetYValueLinear (float _Xvalue)
    {
        return (2 * _Xvalue) + 1;
    }
    public static float GetYQuadFunction(float _a, float _b, float _c,float _XValue)
    {
        return((_a * Mathf.Pow(_XValue, 2)) + (_b * _XValue) + _c);
    }
}