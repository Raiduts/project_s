using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChangeDimension : QuestBase
{
    public override void OnStartQuest()
    {
        //ArrayListOperator.instance.SetArrayDimension(true);
        //print("Starting Quest Dimensi");

        Dudu.Instance.ShowDudu("Okay sekarang coba beralih ke mode 2D");
    }

    public override void OnChangeDimension(ArrayDimension dimension)
    {
         //base.OnChangeDimension(dimension);
        
        if (dimension == ArrayDimension.TwoDimension)
        {
            Dudu.Instance.ShowDudu("Sipp! Sekarang kita sudah beralih ke mode 2D!");

            QuestEvent.CompletedQuest?.Invoke();        
        }
    }
}
