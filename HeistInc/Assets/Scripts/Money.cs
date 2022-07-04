using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money 
{


    public static string MoneyScore(float RawMoneyX, List<string> SymbolArray)
    {

        string result = "";
        int MoneyCharCount;
        int RawMoney = (int)RawMoneyX; 

        string tempMoney = RawMoney.ToString();

        char[] chararr = tempMoney.ToCharArray(); 
        MoneyCharCount = chararr.Length;
        for (int i = 0; i < SymbolArray.Count; i++)
        {
            int x = (i + 1) * 3;

            if (MoneyCharCount <= 3)
            {
                result = RawMoney.ToString();

            }
            else if (MoneyCharCount <= x)
            {
                char[] temp = new char[6];
                string money = "";
                result = RawMoney.ToString();
                for (int j = 0; j < 3; j++)
                {

                    if (MoneyCharCount == x - j)
                    {
                        int charIndexCount = 0;

                        for (int k = 0; k < 5; k++)
                        {
                            if (k == (3 - j))
                            {
                                temp[k] = '.';
                                money = string.Concat(money, ".");

                            }
                            else
                            {
                                money = string.Concat(money, chararr[charIndexCount]);
                                temp[k] = chararr[charIndexCount];
                                charIndexCount++;
                            } 
                        }
                        result = string.Concat(money, SymbolArray[i]);
                    }
                }

                break;
            }
        }
        return result;
    }
}
