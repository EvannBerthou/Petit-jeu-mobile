using System;

public class RoundValue {

    public static string Round(float value) {
        bool negative = value < 0;
        value = Math.Abs(value);

        if(value < 1000)
            return (negative ? "-" : "") + Math.Round(value, 1) + " $";
        else if(value >= 1e3 && value < 1e6)
            return (negative ? "-" : "") + Math.Round(value / 1e3, 1).ToString("0.#k $");
        else if(value >= 1e6 && value < 1e9)
            return (negative ? "-" : "") + Math.Round(value / 1e6, 1).ToString("0.#M $");
        else if(value >= 1e9 && value < 1e12)
            return (negative ? "-" : "") + Math.Round(value / 1e9, 1).ToString("0.#G $");
        else
            return (negative ? "-" : "") + Math.Round(value / 1e12, 1).ToString("0.#T $");
    }
}
