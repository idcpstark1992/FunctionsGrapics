using UnityEngine;

public class PrintNumber : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshPro printCurrentNumber; 
    public void InitNumber (float _innervalue)
    {
        printCurrentNumber.text = _innervalue.ToString();
    }
}
