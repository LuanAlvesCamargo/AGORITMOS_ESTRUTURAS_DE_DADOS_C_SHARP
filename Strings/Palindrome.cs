using System;
using System.Text.RegularExpressions;

namespace Algorithms.Strings;

public static class Palindromo {
    public static bool EhPalindromo(string palavra) =>
        TipificarString(palavra).Equals(TipificarString(InverterString(palavra)));

    private static string TipificarString(string palavra) =>
        Regex.Replace(palavra.ToLowerInvariant(), @"\s+", string.Empty);

    private static string InverterString(string s) {
        var arr = s.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
}